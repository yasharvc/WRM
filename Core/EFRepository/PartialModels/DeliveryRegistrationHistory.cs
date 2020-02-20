using Contracts;
using System;
using System.Linq.Expressions;

namespace EFRepository
{
	public partial class DeliveryRegistrationHistory : PersistableModel<DeliveryRegistrationHistory>
	{
		public override Expression<Func<DeliveryRegistrationHistory, bool>> GetFindPredicate()
		{
			return m => m.CIF == CIF;
		}

		public override void SetDefaultValues()
		{
			CreationDate = CreationDate.Year == 1 ? DateTime.Now : CreationDate;
			LastModifictionDate = LastModifictionDate.Year == 1 ? DateTime.Now : LastModifictionDate;
			DeliveryMode = string.IsNullOrEmpty(DeliveryMode) ? "DEMO VALUE FOR DELIVERY" : DeliveryMode;
			ApprovalStatus = string.IsNullOrEmpty(ApprovalStatus) ? "DEMO VALUE FOR ApprovalStatus" : ApprovalStatus;
			MakerId = string.IsNullOrEmpty(MakerId) ? "DEMO VALUE FOR MakerId" : MakerId;
			RequestStatus = string.IsNullOrEmpty(RequestStatus) ? "DEMO567890" : RequestStatus;
		}

		public override void UpdateValues(DeliveryRegistrationHistory data)
		{
			if (!string.IsNullOrEmpty(AcountNumber))
				data.AcountNumber = AcountNumber;
			data.DeliveryMode = DeliveryMode;
			data.MobileNumber = MobileNumber;
			data.FaxNumber = FaxNumber;
			if (!string.IsNullOrEmpty(PrimaryEmail))
				data.PrimaryEmail = PrimaryEmail;
			data.SecondaryEmail = SecondaryEmail;
			data.SubForAccount = SubForAccount;
			data.SubForCreditCard = SubForCreditCard;
			data.SubForRemittance = SubForRemittance;
			data.SubForDeposit = SubForDeposit;
			data.SubForInv = SubForInv;
		}

		public static implicit operator DeliveryRegistrationHistory(DeliveryRegistration d)
		{
			return new DeliveryRegistrationHistory
			{
				AcountNumber = d.AcountNumber,
				ApprovalStatus = d.ApprovalStatus,
				CheckerId = d.CheckerId,
				CIF = d.CIF,
				Comments = d.Comments,
				CreationDate = d.CreationDate,
				CreditCardNumber = d.CreditCardNumber,
				CRNN = d.CRNN,
				DeliveryMode = d.DeliveryMode,
				ECRN = d.ECRN,
				FaxNumber = d.FaxNumber,
				FirstName = d.FirstName,
				HistoryDateTime = DateTime.Now,
				Id = d.Id,
				LastModifictionDate = d.LastModifictionDate,
				LastName = d.LastName,
				MakerId = d.MakerId,
				ActionCode = -1,
				MasterNumber = d.MasterNumber,
				MiddleName = d.MiddleName,
				MobileNumber = d.MobileNumber,
				ModeOfRegistration = d.ModeOfRegistration,
				ModificationId = d.ModificationId,
				Modifications = d.Modifications,
				PrimaryEmail = d.PrimaryEmail,
				RequestStatus = d.RequestStatus,
				SecondaryEmail = d.SecondaryEmail,
				SubForAccount = d.SubForAccount,
				SubForCreditCard = d.SubForCreditCard,
				SubForDeposit = d.SubForDeposit,
				SubForInv = d.SubForInv,
				SubForJointAccount = d.SubForJointAccount,
				SubForRemittance = d.SubForRemittance,
				SubForTF = d.SubForTF
			};
		}
	}
}