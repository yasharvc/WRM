using FileParser;
using System;
using System.Linq;
using Helper;
using FileParser.RAKBANK;
using System.IO;
using System.Data.Entity.Validation;
using NLog;
using TestClass;

namespace ConsoleOutput
{
	class Program
	{
		static Logger logger = LogManager.GetCurrentClassLogger();
		static System.Timers.Timer timer = new System.Timers.Timer(1000);
		
		class b:a
		{
			public int Age { get; set; }
		}
		static void Main(string[] args)
		{
			FileWriter.ElixirFinacleModel m = new FileWriter.ElixirFinacleModel
			{
				CIFID = "1",
				DeliveryMode = "2",
				FaxNum = "3",
				OperationFlag = "4",
				PrimaryEmail = "5",
				RCreId = "6",
				RCreTime = "7",
				RecordType = "8",
				Request_Status = "9",
				SecondaryEmail = "10",
				SubForAccount = true,
				SubforCC = false,
				SubForDeposit = true,
				SubForRemittance = false,
				SubForFutureService1 = true,
				SubForFutureService2 = false,
				SubForFutureService3 = true,
				SubForInvestments = false
			};
			b b = new b
			{
				Age = 150,
				Id = 10,
				Name = "Yashar"
			};
			a a = b;
			Printa(b);
			Printa(a);
			//Console.WriteLine(new FileWriter.ElixirFinacleWriter().MakeOutPut(Contracts.DownloadFileTypes.FinacleBanking, m));
			Console.ReadKey();
		}

		static void Printa(a a)
		{
			Console.WriteLine(a.Name);
		}

		private static void IndexedFileTest()
		{
			var path = @"D:\CDLEST.txt";
			var hash = path.CalcMD5HashByPath();
			WholeContentParser parser = new WholeContentParser();
			parser.RegisterReader(new CAPSFileReader(), new DigitalBankingFileReader(), new FinaclePasswordExtractionFileReader());
			var lst = parser.ParseFile<EFRepository.DeliveryRegistration>(Path.GetFileName(path), File.ReadAllText(path));
			try
			{
				EFRepository.RAKEntities context = new EFRepository.RAKEntities();
				context.DeliveryRegistrations.AddRange(lst.ToArray());
				context.SaveChanges();
			}
			catch (DbEntityValidationException e)
			{
				foreach (var eve in e.EntityValidationErrors)
				{
					Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
						eve.Entry.Entity.GetType().Name, eve.Entry.State);
					foreach (var ve in eve.ValidationErrors)
					{
						Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
							ve.PropertyName, ve.ErrorMessage);
					}
				}
				throw;
			}
		}
	}
}