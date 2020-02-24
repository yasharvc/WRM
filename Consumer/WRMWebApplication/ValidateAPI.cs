using System;
using System.Linq;
using WRMWebApplication.Models;

namespace WRMWebApplication
{
	public class ValidateAPI
	{
		public CustomerInformation Validate(string cardNumber,string accountNumber)
		{
			if (!string.IsNullOrEmpty(cardNumber))
			{
				return ValidateByCardNumber(cardNumber);
			}
			else if(!string.IsNullOrEmpty(accountNumber))
			{
				return ValidateByAccountNumber(accountNumber);
			}
			throw new ArgumentException();
		}

		private CustomerInformation ValidateByAccountNumber(string accountNumber)
		{
			using (var ctx = new EFRepository.RAKEntities())
			{
				try
				{
					var customer = ctx.DeliveryRegistrations.Single(m => m.AcountNumber == accountNumber);
					return customer;
				}
				catch
				{
					return new CustomerInformation("Account number is not valid");
				}
			}
		}

		private CustomerInformation ValidateByCardNumber(string cardNumber)
		{
			using (var ctx = new EFRepository.RAKEntities())
			{
				try
				{
					var customer = ctx.DeliveryRegistrations.Single(m => m.CreditCardNumber == cardNumber);
					return customer;
				}
				catch(Exception e)
				{
					return new CustomerInformation("Card number is not valid");
				}
			}
		}
	}
}