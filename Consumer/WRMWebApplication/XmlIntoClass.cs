using System;
using EFRepository;
using Helper;

namespace WRMWebApplication
{
	public class XmlIntoClass
	{
		public DeliveryRegistration ToDeliveryRegistrationTextFile(string xml)
		{
			var helper = new XMLHelper(xml);
			helper.GotoPath("FIXML/Body/executeFinacleScriptResponse/executeFinacleScript_CustomData/getCustomerAndAccountDetails_RES");
			var res = new DeliveryRegistration
			{
				AcountNumber = helper.GetInnerTextOf("/AcctDet/AcctId"),
				CIF="",
				FaxNumber="",
				FirstName = helper.GetInnerTextOf("/CustDet/FirstName"),
				LastName = helper.GetInnerTextOf("/CustDet/FirstName"),
				MobileNumber=helper.GetInnerTextOf("/CustDet/MobileNum1"),
				PrimaryEmail= helper.GetInnerTextOf("/CustDet/ElixirPrimaryEmailId"),
				SecondaryEmail= helper.GetInnerTextOf("/CustDet/ElixirSecEmailId"),
				SubForAccount= ToBoolean(),
				SubForCreditCard =ToBoolean(),
				SubForDeposit =ToBoolean(),
				SubForInv = ToBoolean(),
				SubForTF=ToBoolean(),
				SubForRemittance=ToBoolean(),
				SubForJointAccount=ToBoolean(),
				CreditCardNumber = "",
				
			};
			return res;
		}

		private bool? ToBoolean(string v = "")
		{
			if (string.IsNullOrEmpty(v))
				return null;
			return (v == "Y" || v == "y");
		}
	}
}