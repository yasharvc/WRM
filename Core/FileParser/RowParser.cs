using FileParser.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using Helper;
using Contracts;

namespace FileParser
{
    public class RowParser
    {
		public List<String> Slices { get; private set; }
		public int Count
		{
			get
			{
				return Slices.Count;
			}
		}
		public bool HasNumber { get; private set; }
		public int ExcpectedColumnCount { get; private set; }

		public RowParser(int excpectedColumnCount) { ExcpectedColumnCount = excpectedColumnCount; Slices = new List<string>(); }

		public RowParser() : this(0) { }
		public void Parse(string rawRow)
		{
			HasNumber = false;
			var oneLine = rawRow.GetFirstLine(FileContentConsts.NewLine);
			if (rawRow.Trim().Length == 0)
				return;
			var slices = oneLine.Split(new string[] { FileContentConsts.ColumnDelimeter }, StringSplitOptions.None);
			if(ExcpectedColumnCount > 0)
			{
				if (ExcpectedColumnCount != slices.Length)
					throw new ColumnCountDoesntMatchException();
			}
			else
				ExcpectedColumnCount = slices.Length;
			Slices = new List<string>();
			Slices.AddRange(slices);
			HasNumber = Slices.Any(str => str.All(ch => char.IsNumber(ch)));
		}
    }
}