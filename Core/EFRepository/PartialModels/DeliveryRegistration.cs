using Contracts;
using Contracts.Attribute;
using Helper.ColumnConverter;
using System;
using System.Linq.Expressions;

namespace EFRepository
{
	public partial class DeliveryRegistration : PersistableModel<DeliveryRegistration>
	{

		[FileToPropertyMapper(UploadFileTypes.RAKBANK_CAPS, "CIFID")]
		[FileToPropertyMapper(UploadFileTypes.DigitalBanking, "CIFID")]
		[FileToPropertyMapper(UploadFileTypes.Finacle_Premium_Bank, "CIFID")]
		public string CIF { get; set; }

		[FileToPropertyMapper(UploadFileTypes.RAKBANK_CAPS, "CCEcrnNo")]
		public string ECRN { get; set; }

		[FileToPropertyMapper(UploadFileTypes.Finacle_Premium_Bank, "AcctNum")]
		public string AcountNumber { get; set; }

		[FileToPropertyMapper(UploadFileTypes.RAKBANK_CAPS, "DeliveryMode")]
		[FileToPropertyMapper(UploadFileTypes.DigitalBanking, "DeliveryMode")]
		[FileToPropertyMapper(UploadFileTypes.Finacle_Premium_Bank, "DeliveryMode")]
		public string DeliveryMode { get; set; }

		[FileToPropertyMapper(UploadFileTypes.RAKBANK_CAPS, "MobileNum")]
		[FileToPropertyMapper(UploadFileTypes.DigitalBanking, "MobileNum")]
		[FileToPropertyMapper(UploadFileTypes.Finacle_Premium_Bank, "MobileNum")]
		public string MobileNumber { get; set; }

		[FileToPropertyMapper(UploadFileTypes.RAKBANK_CAPS, "FaxNum")]
		[FileToPropertyMapper(UploadFileTypes.DigitalBanking, "FaxNum")]
		[FileToPropertyMapper(UploadFileTypes.Finacle_Premium_Bank, "FaxNum")]
		public string FaxNumber { get; set; }

		[FileToPropertyMapper(UploadFileTypes.RAKBANK_CAPS, "PrimaryEmail")]
		[FileToPropertyMapper(UploadFileTypes.DigitalBanking, "PrimaryEmail")]
		[FileToPropertyMapper(UploadFileTypes.Finacle_Premium_Bank, "PrimaryEmail")]
		public string PrimaryEmail { get; set; }
		[FileToPropertyMapper(UploadFileTypes.RAKBANK_CAPS, "SecondaryEmail")]
		[FileToPropertyMapper(UploadFileTypes.DigitalBanking, "SecondaryEmail")]
		[FileToPropertyMapper(UploadFileTypes.Finacle_Premium_Bank, "SecondaryEmail")]
		public string SecondaryEmail { get; set; }

		[FileToPropertyMapper(UploadFileTypes.RAKBANK_CAPS, "SubForAccount", typeof(YesNoToBooleanConverter))]
		[FileToPropertyMapper(UploadFileTypes.DigitalBanking, "SubForAccount", typeof(YesNoToBooleanConverter))]
		[FileToPropertyMapper(UploadFileTypes.Finacle_Premium_Bank, "SubForAccount", typeof(YesNoToBooleanConverter))]
		public Nullable<bool> SubForAccount { get; set; }

		[FileToPropertyMapper(UploadFileTypes.DigitalBanking, "RCreId")]
		public string LastName { get; set; }

		//-----------------------------------------------------------------------------------------------
		//-----------------------------------------------------------------------------------------------
		//-----------------------------------------------------------------------------------------------
		//-----------------------------------------------------------------------------------------------



		[FileToPropertyMapper(UploadFileTypes.RAKBANK_CAPS, "SubforCC", typeof(YesNoToBooleanConverter))]
		[FileToPropertyMapper(UploadFileTypes.DigitalBanking, "SubforCC", typeof(YesNoToBooleanConverter))]
		[FileToPropertyMapper(UploadFileTypes.Finacle_Premium_Bank, "SubforCC", typeof(YesNoToBooleanConverter))]
		public Nullable<bool> SubForCreditCard { get; set; }

		[FileToPropertyMapper(UploadFileTypes.RAKBANK_CAPS, "SubForRemittance", typeof(YesNoToBooleanConverter))]
		[FileToPropertyMapper(UploadFileTypes.DigitalBanking, "SubForRemittance", typeof(YesNoToBooleanConverter))]
		[FileToPropertyMapper(UploadFileTypes.Finacle_Premium_Bank, "SubForRemittance", typeof(YesNoToBooleanConverter))]
		public Nullable<bool> SubForRemittance { get; set; }

		[FileToPropertyMapper(UploadFileTypes.RAKBANK_CAPS, "SubForDeposit", typeof(YesNoToBooleanConverter))]
		[FileToPropertyMapper(UploadFileTypes.DigitalBanking, "SubForDeposit", typeof(YesNoToBooleanConverter))]
		[FileToPropertyMapper(UploadFileTypes.Finacle_Premium_Bank, "SubForDeposit", typeof(YesNoToBooleanConverter))]
		public Nullable<bool> SubForDeposit { get; set; }
		public Nullable<bool> SubForJointAccount { get; set; }

		[FileToPropertyMapper(UploadFileTypes.RAKBANK_CAPS, "SubForInvestments", typeof(YesNoToBooleanConverter))]
		[FileToPropertyMapper(UploadFileTypes.DigitalBanking, "SubForInvestments", typeof(YesNoToBooleanConverter))]
		[FileToPropertyMapper(UploadFileTypes.Finacle_Premium_Bank, "SubForInvestments", typeof(YesNoToBooleanConverter))]
		public Nullable<bool> SubForInv { get; set; }


		public override void SetDefaultValues()
		{
			CreationDate = CreationDate.Year == 1 ? DateTime.Now : CreationDate;
			LastModifictionDate = LastModifictionDate.Year == 1 ? DateTime.Now : LastModifictionDate;
			DeliveryMode = IfIsNullReturnEmptyString(DeliveryMode);
			ApprovalStatus = IfIsNullReturnEmptyString(ApprovalStatus);
			MakerId = string.IsNullOrEmpty(MakerId) ? "" : MakerId;
			RequestStatus = string.IsNullOrEmpty(RequestStatus) ? "SUC" : RequestStatus;

			CRNN = IfIsNullReturnEmptyString(CRNN);
			FirstName = IfIsNullReturnEmptyString(FirstName);
			LastName = IfIsNullReturnEmptyString(LastName);
			MiddleName = IfIsNullReturnEmptyString(MiddleName);
			SubForTF = false;
			SubForJointAccount = false;
			CreditCardNumber = "";
			MasterNumber = "";
			ModificationId = null;
			Comments = "";
		}

		private string IfIsNullReturnEmptyString(string input,string emptyValue = "")
		{
			return string.IsNullOrEmpty(input) ? emptyValue : input;
		}

		public override Expression<Func<DeliveryRegistration, bool>> GetFindPredicate()
		{
			return m => m.CIF == CIF;
		}

		public override void UpdateValues(DeliveryRegistration data)
		{
			if (!string.IsNullOrEmpty(AcountNumber))
				data.AcountNumber = AcountNumber;
			data.DeliveryMode = DeliveryMode;
			data.MobileNumber = MobileNumber;
			data.FaxNumber = FaxNumber;
			if(!string.IsNullOrEmpty(PrimaryEmail))
				data.PrimaryEmail = PrimaryEmail;
			data.SecondaryEmail = SecondaryEmail;
			data.SubForAccount = SubForAccount;
			data.SubForCreditCard = SubForCreditCard;
			data.SubForRemittance = SubForRemittance;
			data.SubForDeposit = SubForDeposit;
			data.SubForInv = SubForInv;
		}
	}
}