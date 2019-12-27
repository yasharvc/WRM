using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper;
using FileParser.Exception;

namespace FileParser
{
	public class WholeContentParser
	{
		public IEnumerable<string> Lines { get; private set; }
		protected List<FileReader> Readers { get; set; }
		protected bool HasHeaderFooter { get; set; }

		public WholeContentParser()
		{
			Readers = new List<FileReader>();
			Lines = new List<string>();
		}

		public IEnumerable<T> ParseFile<T>(string fileName, string content) where T : PersistableModel<T>, new()
		{
			var firstLine = content.GetFirstLine(FileContentConsts.NewLine);
			var lineParser = new RowParser();
			lineParser.Parse(firstLine);
			HasHeaderFooter = lineParser.Slices[0].Equals("H", StringComparison.OrdinalIgnoreCase);
			if (HasHeaderFooter)
			{
				try
				{
					return ParseFileWithHeaderFooter<T>(fileName, content, firstLine);
				}
				catch (HeaderFooterFileIsCorruptedException)
				{
					throw new HeaderFooterFileIsCorruptedException(fileName, firstLine, content);
				}
			}
			else
			{
				try
				{
					return ParseFileWithoutHeaderFooter<T>(fileName, content, firstLine);
				}
				catch (HeaderFooterFileIsCorruptedException)
				{
					throw new HeaderFooterFileIsCorruptedException(fileName, firstLine, content);
				}
			}
		}

		private IEnumerable<T> ParseFileWithHeaderFooter<T>(string fileName, string content, string firstLine) where T : PersistableModel<T>, new()
		{
			var lastLine = content.GetLastLine(FileContentConsts.NewLine);
			var lineParser = new RowParser(2);
			lineParser.Parse(lastLine);
			try
			{
				var cnt = int.Parse(lineParser.Slices[1]);
				content = content.SkipHeaderFooter(FileContentConsts.NewLine);
				var reader = GetEligableReader(fileName, firstLine);
				if (reader == null)
					throw new SuitableFileReaderNotFoundException(fileName, firstLine, content);

				return reader.Parse<T>(content);
			}
			catch
			{
				throw new HeaderFooterFileIsCorruptedException("", "");
			}
		}

		public FileReader GetEligableReader(string fileName, string firstLine)
		{
			var reader = Readers.FirstOrDefault(m => m.IsEligableForFile(fileName, firstLine));
			return reader;
		}

		private IEnumerable<T> ParseFileWithoutHeaderFooter<T>(string fileName, string content, string firstLine) where T : PersistableModel<T>, new()
		{
			var reader = Readers.FirstOrDefault(m => m.IsEligableForFile(fileName, firstLine));
			if (reader == null)
				throw new SuitableFileReaderNotFoundException(fileName, firstLine, content);

			reader.ParseColumnNames(firstLine);
			return reader.Parse<T>(content.SkipFirstLine(FileContentConsts.NewLine));
		}

		public void RegisterReader(params FileReader[] readers)
		{
			foreach (var reader in readers)
				Readers.Add(reader);
		}
	}
}
