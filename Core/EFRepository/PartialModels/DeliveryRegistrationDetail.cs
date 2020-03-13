using Contracts;
using Contracts.Attribute;
using System;
using System.Linq.Expressions;

namespace EFRepository
{
	public partial class DeliveryRegistrationDetail : PersistableModel<DeliveryRegistrationDetail>
	{
		[FileToPropertyMapper(UploadFileTypes.Finacle_Password_Extraction, 1)]
		[FileToPropertyMapper(UploadFileTypes.Finacle_Premium_Bank, 1)]
		public string CIFID { get; set; }
		[FileToPropertyMapper(UploadFileTypes.Finacle_Password_Extraction, 2)]
		public string DateOfBirth { get; set; }
		[FileToPropertyMapper(UploadFileTypes.Finacle_Password_Extraction, 3)]
		public string TradeLicenseNumber { get; set; }
		[FileToPropertyMapper(UploadFileTypes.Finacle_Password_Extraction, 4)]
		public string RetailCorporateIndicator { get; set; }
		public override void SetDefaultValues()
		{
			ModificationDate = CreationDate = DateTime.Now;
		}

		public override Expression<Func<DeliveryRegistrationDetail, bool>> GetFindPredicate()
		{
			return m => m.CIFID == CIFID;
		}

		public override void UpdateValues(DeliveryRegistrationDetail data)
		{
			data.DateOfBirth = DateOfBirth;
			data.TradeLicenseNumber = TradeLicenseNumber;
			data.RetailCorporateIndicator = RetailCorporateIndicator;
		}
	}
}
