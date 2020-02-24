namespace WRMWebApplication.Models
{
	public class CustomerInformation
	{
		public CustomerInformation() { Validated = true; }
		public CustomerInformation(string error)
		{
			Error = error;
			Validated = false;
		}
		public string CIF { get; set; }
		public bool Statement { get; set; }
		public bool FDConfirmation { get; set; }
		public bool Remittance { get; set; }
		public bool JointAccount { get; set; }
		public bool Investments { get; set; }
		public bool HasEmail { get; set; }
		public string PrimaryEmail { get; set; }
		public string SecondaryEmail { get; set; }
		public bool HasFax { get; set; }
		public string FaxNumber { get; set; }

		public bool Validated { get; set; }
		public string Error { get; set; }
		public string Name { get; set; }

		public static implicit operator CustomerInformation(EFRepository.DeliveryRegistration d)
		{
			return new CustomerInformation
			{
				CIF = d.CIF,
				Error = "",
				FaxNumber = d.FaxNumber,
				HasEmail = !string.IsNullOrEmpty(d.PrimaryEmail) || !string.IsNullOrEmpty(d.SecondaryEmail),
				HasFax = !string.IsNullOrEmpty(d.FaxNumber),
				PrimaryEmail = string.IsNullOrEmpty(d.PrimaryEmail) ? "" : d.PrimaryEmail,
				Investments = GetValueFromNullable(d.SubForInv),
				JointAccount = GetValueFromNullable(d.SubForJointAccount),
				Remittance = GetValueFromNullable(d.SubForRemittance),
				Statement = GetValueFromNullable(d.SubForAccount),
				FDConfirmation = GetValueFromNullable(d.SubForDeposit),
				SecondaryEmail = string.IsNullOrEmpty(d.SecondaryEmail) ? "" : d.SecondaryEmail,
				Validated = true,
				Name = d.FirstName + " - " + d.LastName
			};
		}

		private static bool GetValueFromNullable(bool? d)
		{
			return d.HasValue ? d.Value : false;
		}
	}
}