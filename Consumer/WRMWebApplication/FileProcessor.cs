using System;
using System.Linq;
using System.Threading;
using Helper;
using FileParser;
using FileParser.RAKBANK;
using System.IO;
using System.Data.Entity.Validation;
using EFRepository;
using System.Collections.Generic;

namespace WRMWebApplication
{
	public enum Status
	{
		CheckingFile,
		Processing,
		Finished
	}

	public enum ProcessResult
	{
		AlreadyProcessed,
		Corrupted,
		Processed
	}
	internal class FileProcessor
	{
		public event EventHandler<ProcessResult> OnProcessResult;
		private void _OnProcessResult()
		{
			if (OnProcessResult != null)
				OnProcessResult(this, Result);
		}
		public string PathToFile { get; set; }
		private EFRepository.RAKEntities Context { get; set; }
		public ProcessResult Result { get; private set; }
		public FileProcessor(string path)
		{
			PathToFile = path;
			Context = new EFRepository.RAKEntities();
		}
		public Status Status { get; set; }
		public void Process()
		{
			//var thread = new Thread(new ThreadStart(() => {
			//	JobToDo();
			//}));
			//thread.Start();
			JobToDo();
		}

		private void JobToDo()
		{
			if(Context.Database.Connection.State != System.Data.ConnectionState.Open)
				Context.Database.Connection.Open();
			Status = Status.CheckingFile;
			var hash = PathToFile.CalcMD5HashByPath();
			if (Context.ProcessedFiles.Any(m => m.FileName.Equals(PathToFile) || m.MD5Hash == hash))
			{
				Status = Status.Finished;
				ChangeResultWithEvent(ProcessResult.AlreadyProcessed);
				return;
			}
			Status = Status.Processing;
			try
			{
				ProcessContent();
				ChangeResultWithEvent(ProcessResult.Processed);
			}
			catch (DbEntityValidationException e)
			{
				ChangeResultWithEvent(ProcessResult.Corrupted);
				throw e;
			}
			catch
			{
				ChangeResultWithEvent(ProcessResult.Corrupted);
			}
			Status = Status.Finished;
		}

		private void ChangeResultWithEvent(ProcessResult res)
		{
			Result = res;
			_OnProcessResult();
		}

		private void ProcessContent()
		{
			var path = PathToFile;
			WholeContentParser parser = new WholeContentParser();
			parser.RegisterReader(
				  new CAPSFileReader()
				, new DigitalBankingFileReader()
				, new FinaclePasswordExtractionFileReader()
				, new FinacleCustomerFileReader()
				, new FinaclePremiumBankingFileReader());

			var reader = parser.GetEligableReader(System.IO.Path.GetFileName(path), File.ReadAllText(path));
			try
			{
				RAKEntities context = new EFRepository.RAKEntities();
				ProcessedFile processFileInfo = new EFRepository.ProcessedFile();
				if (reader.GetType() == typeof(CAPSFileReader) ||
					reader.GetType() ==  typeof(DigitalBankingFileReader) ||
					reader.GetType() == typeof(FinacleCustomerFileReader))
				{
					var lst = parser.ParseFile<DeliveryRegistration>(System.IO.Path.GetFileName(path), File.ReadAllText(path));
					foreach (var reg in lst)
					{
						reg.SetDefaultValues();
						if (reg.GetOperation() == Contracts.OperationFlag.Insert)
						{
							reg.MakerId = reg.CheckerId = "WRM";
							context.DeliveryRegistrations.Add(reg);
						}
						else if (reg.GetOperation() == Contracts.OperationFlag.Modification)
						{
							var res = Context.DeliveryRegistrations.SingleOrDefault(reg.GetFindPredicate());
							DeliveryRegistrationHistory history = res;
							history.ActionCode = 1;//Update
							reg.UpdateValues(res);
							reg.LastModifictionDate = DateTime.Now;
							reg.MakerId = reg.CheckerId = "WRM";
							context.DeliveryRegistrationHistories.Add(history);
						}
						else if (reg.GetOperation() == Contracts.OperationFlag.Unsupscription)
						{
							var res = Context.DeliveryRegistrations.SingleOrDefault(reg.GetFindPredicate());
							DeliveryRegistrationHistory history = res;
							history.ActionCode = 2;//Delete
							context.DeliveryRegistrations.Remove(res);
							context.DeliveryRegistrationHistories.Add(history);
						}
					}
				}
				else if (reader is FinaclePasswordExtractionFileReader || reader is FinaclePremiumBankingFileReader)
				{
					var lst = parser.ParseFile<DeliveryRegistrationDetail>(System.IO.Path.GetFileName(path), File.ReadAllText(path));
					foreach (var reg in lst)
					{
						reg.SetDefaultValues();
						if (reg.GetOperation() == Contracts.OperationFlag.Insert)
							context.DeliveryRegistrationDetails.Add(reg);
						else if (reg.GetOperation() == Contracts.OperationFlag.Modification)
						{
							var res = Context.DeliveryRegistrationDetails.SingleOrDefault(reg.GetFindPredicate());
							reg.UpdateValues(res);
						}
						else if (reg.GetOperation() == Contracts.OperationFlag.Unsupscription)
						{
							var res = Context.DeliveryRegistrationDetails.SingleOrDefault(reg.GetFindPredicate());
							context.DeliveryRegistrationDetails.Remove(res);
						}
					}
				}
				else
				{

				}
				processFileInfo.FileName = path;
				processFileInfo.MD5Hash = path.CalcMD5HashByPath();
				processFileInfo.Date = DateTime.Now;
				context.ProcessedFiles.Add(processFileInfo);
				context.SaveChanges();
			}
			catch (DbEntityValidationException e)
			{
				throw e;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}