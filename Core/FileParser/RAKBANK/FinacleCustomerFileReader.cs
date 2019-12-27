using Contracts;
using System;

namespace FileParser.RAKBANK
{
	public class FinacleCustomerFileReader : CAPSFileReader
	{
		public override bool IsEligableForFile(string fileName, string firstLineOfFile = null)
		{
			return fileName.StartsWith("FIN_ELXR_EXTCUSTDTL_DAILY_NEW", StringComparison.OrdinalIgnoreCase);
		}
		public FinacleCustomerFileReader()
			: base()
		{
			ExceptedColumnCount = 42;
			FileType = UploadFileTypes.Finacle_Customer_Detail;
		}
	}
}