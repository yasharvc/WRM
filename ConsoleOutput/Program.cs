using FileParser;
using FileParser.RAKBANK;
using Helper;
using NLog;
using System;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text;
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

			var xml = new XMLTest();
			xml.GotoPath("FIXML/Body/executeFinacleScriptResponse/executeFinacleScript_CustomData/getCustomerAndAccountDetails_RES/CustDet");

			Console.WriteLine(xml.GetInnerTextOf("FirstName"));
			Console.WriteLine(xml.GetInnerTextOf("LastName"));
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