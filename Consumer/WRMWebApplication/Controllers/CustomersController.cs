using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EFRepository;

namespace WRMWebApplication.Controllers
{
	public class CustomersController : ApiController
	{
		RAKEntities context = new RAKEntities();
		[HttpGet]
		public IQueryable<DeliveryRegistration> GetAll()
		{
			return context.DeliveryRegistrations;
		}
	}
}