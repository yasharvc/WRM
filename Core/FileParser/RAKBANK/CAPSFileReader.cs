using Contracts;
using FileParser.Exception;
using System;
using System.Collections.Generic;

namespace FileParser.RAKBANK
{
	public class CAPSFileReader : FileReader
	{
		public override bool IsEligableForFile(string fileName, string firstLineOfFile = null)
		{
			return fileName.StartsWith("CDLEST", StringComparison.OrdinalIgnoreCase);
		}

		public CAPSFileReader() : base(20, UploadFileTypes.RAKBANK_CAPS) { }

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
						temp.SetOperation(OperationFlag.Insert);
					else if (parser.Slices[1].ToLower() == "m")
						temp.SetOperation(OperationFlag.Modification);
					else if (parser.Slices[1].ToLower() == "d")
						temp.SetOperation(OperationFlag.Unsupscription);
					else
						temp.SetOperation(OperationFlag.Insert);
					res.Add(temp);
				}
				catch (ColumnCountDoesntMatchException)
				{
					throw;
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