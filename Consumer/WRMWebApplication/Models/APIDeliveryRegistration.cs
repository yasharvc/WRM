using EFRepository;

namespace WRMWebApplication.Models
{
	public class APIDeliveryRegistration : DeliveryRegistration
	{
		public string Action { get; set; }

		public DeliveryRegistration ToBase()
		{
			return new DeliveryRegistration
			{
				AcountNumber = AcountNumber,
				ApprovalStatus = ApprovalStatus,
				CheckerId = CheckerId,
				CIF = CIF,
				Comments = Comments,
				CreationDate = CreationDate,
				CreditCardNumber = CreditCardNumber,
				CRNN = CRNN,
				DeliveryMode = DeliveryMode,
				ECRN = ECRN,
				FaxNumber = FaxNumber,
				FirstName = FirstName,
				Id = Id,
				LastModifictionDate = LastModifictionDate,
				LastName = LastName,
				MakerId = MakerId,
				MasterNumber = MasterNumber,
				MiddleName = MiddleName,
				MobileNumber = MobileNumber,
				ModeOfRegistration = ModeOfRegistration,
				ModificationId = ModificationId,
				Modifications = Modifications,
				PrimaryEmail = PrimaryEmail,
				RequestStatus = RequestStatus,
				SecondaryEmail = SecondaryEmail,
				SubForAccount = SubForAccount,
				SubForCreditCard = SubForCreditCard,
				SubForDeposit = SubForDeposit,
				SubForInv = SubForInv,
				SubForJointAccount = SubForJointAccount,
				SubForRemittance = SubForRemittance,
				SubForTF = SubForTF
			};
		}
	}
}