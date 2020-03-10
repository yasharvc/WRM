using EFRepository;
using System;
using System.Linq;
using System.Web.Mvc;
using WRMWebApplication.Models;

namespace WRMWebApplication.Controllers
{
	[Authorize]
	public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult New()
        {
            return View();
        }

		[HttpPost]
		public JsonResult Validate(string accountNumber,string cardNumber)
		{
			return Json(new ValidateAPI().Validate(cardNumber, accountNumber));
		}

		[HttpPost]
		public JsonResult UpdateInformation(CustomerInformation info)
		{
			using (var ctx = new RAKEntities())
			{
				var customer = ctx.DeliveryRegistrations.FirstOrDefault(m => m.CIF == info.CIF);
				if (customer == null)
					return InsertCustomer(ctx, info);
				else
					return UpdateCustomer(ctx, customer, info);
				
			}
		}

		private JsonResult UpdateCustomer(RAKEntities ctx, DeliveryRegistration customer, CustomerInformation info)
		{
			if (!IsValueChanged(customer, info))
			{
				return Json(new { result = false, Error = "Data in database and input are same" });
			}
			else
			{
				if (info.HasFax)
					customer.FaxNumber = info.FaxNumber;
				if (info.HasEmail)
				{
					customer.PrimaryEmail = info.PrimaryEmail;
					customer.SecondaryEmail = info.SecondaryEmail;
				}
				customer.SubForInv = info.Investments;
				customer.SubForJointAccount = info.JointAccount;
				customer.SubForRemittance = info.Remittance;
				customer.SubForAccount = info.Statement;
				customer.SubForDeposit = info.FDConfirmation;
				ctx.SaveChanges();
				return Json(new { result = true });
			}
		}

		private bool IsValueChanged(DeliveryRegistration customer, CustomerInformation info)
		{
			if (info.HasFax)
				if (customer.FaxNumber != info.FaxNumber)
					return true;
			if (info.HasEmail)
			{
				if (customer.PrimaryEmail != info.PrimaryEmail ||
				customer.SecondaryEmail != info.SecondaryEmail)
					return true;
			}
			if (
			customer.SubForInv != info.Investments ||
			customer.SubForJointAccount != info.JointAccount ||
			customer.SubForRemittance != info.Remittance ||
			customer.SubForAccount != info.Statement ||
			customer.SubForDeposit != info.FDConfirmation)
				return true;
			return false;
		}

		private JsonResult InsertCustomer(RAKEntities ctx, CustomerInformation info) 
		{
			return Json(new { result = false, Error = "Inserting new customer functionality is not available yet" });
		}
	}
}