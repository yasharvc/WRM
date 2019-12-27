using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileParser.RAKBANK
{
	public class FinaclePasswordExtractionFileReader:FileReader
	{
		public override bool IsEligableForFile(string fileName, string firstLineOfFile = null)
		{
			return fileName.StartsWith("fin_extractcifdetailstoelixir_daily_inc_", StringComparison.OrdinalIgnoreCase);
		}
		public FinaclePasswordExtractionFileReader() : base(5, UploadFileTypes.Finacle_Password_Extraction) { }

		public override IEnumerable<T> Parse<T>(string fileContent)
		{
			var mapper = new ColumnMapper();
			T newT = new T();
			var props = newT.GetMappingProperties(UploadFileTypes.Finacle_Password_Extraction);
			var res = new List<T>();
			var parser = new RowParser(ExceptedColumnCount);

			foreach (var prop in props)
			{
				mapper[prop.Index] = prop.PropertyName;
			}
			Mapper = mapper;

			var lines = fileContent.Split(new string[] { FileContentConsts.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			foreach (var line in lines)
			{
				try
				{
					parser.Parse(line);
					res.Add(ToT<T>(Connector.Connect(parser.Slices)));
				}
				catch { }
			}
			return res;
		}
	}
}
