using EFRepository;
using System.Linq;
using System.Web.Http;

namespace WRMWebApplication.Controllers
{
	public class CustomersController : ApiController
	{
		RAKEntities context = new RAKEntities();
		//[HttpGet]
		//public string Get()
		//{
		//	return "ASD";
		//}

		[HttpGet]
		public IQueryable<DeliveryRegistration> GetAll(int id, Models.Data data)
		{
			var x = Request.Headers.GetValues("username");
			var mem = new System.IO.MemoryStream();
			byte[] res = Request.Content.ReadAsByteArrayAsync().Result;
			var str = System.Text.Encoding.UTF8.GetString(mem.ToArray());
			return context.DeliveryRegistrations;
		}
	}
}