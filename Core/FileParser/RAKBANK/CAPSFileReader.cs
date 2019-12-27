using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileParser.RAKBANK
{
	public class CAPSFileReader : FileReader
	{
		public override bool IsEligableForFile(string fileName, string firstLineOfFile = null)
		{
			return fileName.StartsWith("CDLEST", StringComparison.OrdinalIgnoreCase);
		}

		public CAPSFileReader() : base(20, Contracts.UploadFileTypes.RAKBANK_CAPS) { }

		public override IEnumerable<T> Parse<T>(string fileContent)
		{
			var res = new List<T>();
			var parser = new RowParser(ExceptedColumnCount);
			var lines = fileContent.Split(new string[] { FileContentConsts.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			foreach (var line in lines)
			{
				try
				{
					parser.Parse(line);
					T temp = new T();
					temp = ToT<T>(Connector.Connect(parser.Slices));
					temp.SetDefaultValues();
					if (parser.Slices[1].ToLower() == "i")
						temp.SetOperation(Contracts.OperationFlag.Insert);
					if (parser.Slices[1].ToLower() == "m")
						temp.SetOperation(Contracts.OperationFlag.Modification);
					if (parser.Slices[1].ToLower() == "d")
						temp.SetOperation(Contracts.OperationFlag.Unsupscription);
					res.Add(temp);
				}
				catch { }
			}
			return res;
		}

		public override void ParseColumnNames(string firstLine)
		{
			var rowParser = new RowParser();
			rowParser.Parse(firstLine);
			Mapper.AddToEnd(rowParser.Slices.ToArray());
		}
	}
}