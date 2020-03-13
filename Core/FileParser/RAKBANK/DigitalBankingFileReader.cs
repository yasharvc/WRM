using Contracts;
using System;

namespace FileParser.RAKBANK
{
	public class DigitalBankingFileReader:CAPSFileReader
	{
		public override bool IsEligableForFile(string fileName, string firstLineOfFile = null)
		{
			return fileName.StartsWith("DLESTMT", StringComparison.OrdinalIgnoreCase);
		}
		public DigitalBankingFileReader():base()
		{
			ExceptedColumnCount = 14;
			FileType = UploadFileTypes.DigitalBanking;
		}
	}
}