using System;

namespace FileParser.RAKBANK
{
	public class FinaclePremiumBankingFileReader : CAPSFileReader
	{
		public override bool IsEligableForFile(string fileName, string firstLineOfFile = null)
		{
			return fileName.StartsWith("FIN_ELXR_EXTPREMIUMCUSTDTL_DAILY", StringComparison.OrdinalIgnoreCase);
		}

		public FinaclePremiumBankingFileReader()
			: base()
		{
			ExceptedColumnCount = 4;
			FileType = Contracts.UploadFileTypes.Finacle_Premium_Bank;
		}
	}
}
