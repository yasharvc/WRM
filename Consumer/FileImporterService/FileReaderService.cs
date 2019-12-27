using FileImporterService.Classes;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.ServiceProcess;
using System.Timers;
using System.Linq;
using System.Data.Entity.Core;
using NLog.Targets;

namespace FileImporterService
{
	public partial class FileReaderService : ServiceBase
	{
		static Logger logger = LogManager.GetCurrentClassLogger();
		static Configuration Configuration { get; set; }
		Timer Ticker = new Timer(1000);
		Timer ConfigureReaderimer = new Timer(100);
		List<string> FileNames { get; set; }
		bool isInProcess = false;
		public FileReaderService()
		{
			InitializeComponent();
			InitConfiguration();
			InitTimers();
			FileNames = new List<string>();
		}

		private void InitConfiguration()
		{
			Configuration = new Configuration { ConfigurationFileCheckerInterval = 100, TimerInterval = 100, UploadPath = "" };
		}

		private void InitTimers()
		{
			ConfigureReaderimer.Enabled = true;
			Ticker.Enabled = true;
			ConfigureReaderimer.Elapsed += ConfigureReaderimer_Elapsed;
			Ticker.Elapsed += Ticker_Elapsed;
		}

		void Ticker_Elapsed(object sender, ElapsedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(Configuration.UploadPath) || isInProcess)
				return;
			else
			{
				isInProcess = true;
				ProcessDirectory();
			}
		}

		private void ProcessDirectory()
		{
			if (ConfigurationPathIsValid())
			{
				var lst = Directory.GetFiles(Configuration.UploadPath).Except(FileNames);
				foreach (var filePath in lst)
				{
					try
					{
						var processor = new Classes.FileProcessor(filePath);
						processor.OnProcessResult += (obj, res) =>
						{
							if (res == Classes.ProcessResult.Processed)
								logger.Info(string.Format("File '{0}' processed successfully", (obj as FileProcessor).PathToFile));
							else if (res == ProcessResult.Corrupted)
								logger.Error(string.Format("File '{0}' corrupted!!!", (obj as FileProcessor).PathToFile));
							else if (res == ProcessResult.AlreadyProcessed)
								logger.Info(string.Format("File '{0}' already processed", (obj as FileProcessor).PathToFile));
						};
						processor.Process();
					}
					catch (DbEntityValidationException e)
					{
						foreach (var eve in e.EntityValidationErrors)
						{
							var error = (string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
								eve.Entry.Entity.GetType().Name, eve.Entry.State));
							foreach (var ve in eve.ValidationErrors)
							{
								error += "\r\n\t" + string.Format("- Property: \"{0}\", Error: \"{1}\"",
									ve.PropertyName, ve.ErrorMessage);
							}
							logger.ErrorException(error, e);
						}
					}
					catch (EntityCommandExecutionException entityEx)
					{
						logger.ErrorException(entityEx.InnerException.Message, entityEx);
					}
					catch (Exception ex)
					{
						logger.ErrorException("Unexpected error!!!!", ex);
					} 
					FileNames.Add(filePath);
				}
			}
			isInProcess = false;
		}

		private static bool ConfigurationPathIsValid()
		{
			return !(string.IsNullOrEmpty(Configuration.UploadPath) || string.IsNullOrWhiteSpace(Configuration.UploadPath)) && Directory.Exists(Configuration.UploadPath);
		}

		void ConfigureReaderimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			ConfigureReaderimer.Enabled = false;
			try
			{
				var configuration = Newtonsoft.Json.JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "configuration.json")));
				SetConfiguration(configuration);
			}
			catch (Exception ex)
			{
				logger.ErrorException("Exception", ex);
			}
			finally
			{
				ConfigureReaderimer.Enabled = true;
				ConfigureReaderimer.Start();
			}
		}

		private void SetConfiguration(Configuration configuration)
		{
			if (configuration.ConfigurationFileCheckerInterval > 0)
				ConfigureReaderimer.Interval = Configuration.ConfigurationFileCheckerInterval = configuration.ConfigurationFileCheckerInterval;
			if (configuration.TimerInterval > 0 && configuration.TimerInterval != Ticker.Interval)
			{
				Ticker.Interval = Configuration.TimerInterval = configuration.TimerInterval;
				Ticker.Enabled = true;
				Ticker.Start();
			}
			if (!Configuration.UploadPath.Equals(configuration.UploadPath))
			{
				Configuration.UploadPath = configuration.UploadPath;
			}
			if (!string.IsNullOrEmpty(configuration.LogPath))
			{
				var target = (FileTarget)LogManager.Configuration.FindTargetByName("file");
				target.FileName = configuration.LogPath;
				LogManager.ReconfigExistingLoggers();
				Configuration.LogPath = configuration.LogPath;
			}
		}

		protected override void OnStart(string[] args)
		{
			
		}

		protected override void OnStop()
		{
			LogManager.Shutdown();
		}
	}
}