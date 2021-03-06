﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileParser.Exception
{
	public class SuitableFileReaderNotFoundException : Contracts.Exception.ApplicationException
	{
		public string FileName { get; set; }
		public string FirstLine { get; set; }
		public string Content { get; set; }
		public SuitableFileReaderNotFoundException(string fileName,string firstLine,string content ="")
		{
			FileName = fileName;
			FirstLine = firstLine;
			Content = content;
		}
	}
}