using EFRepository;
using System.Linq;
using System.Web.Mvc;

namespace WRMWebApplication.Controllers
{
	public class CheckerController : Controller
	{
		
		// GET: Checker
		public ActionResult Index()
		{
			return View();
		}

		public int PageCount()
		{
			using (var ctx = new RAKEntities())
			{
				return (ctx.DeliveryRegistrations.Where(m => string.IsNullOrEmpty(m.CheckerId)).Count() / RecordsInPage()) + 1;
			}
		}
		public int RecordsInPage()
		{
			return 10;
		}
		public JsonResult GetPageData(int id)
		{
			using (var ctx = new RAKEntities())
			{
				var res = ctx.DeliveryRegistrations.Where(m => string.IsNullOrEmpty(m.CheckerId)).OrderByDescending(m => m.Id).Skip((id - 1) * RecordsInPage()).Take(RecordsInPage()).ToList();
				return Json(res, JsonRequestBehavior.AllowGet);
			}
		}

		public JsonResult GetColumnNames()
		{
			return Json(new string[] { "CIF", "FirstName", "LastName" , "tool" }, JsonRequestBehavior.AllowGet);
		}

		public JsonResult GetColumnTitles()
		{
			return Json(new string[] { "CIF", "First Name", "LastName", "Actions" }, JsonRequestBehavior.AllowGet);
		}

		public JsonResult Check(int id)
		{
			using(var ctx = new RAKEntities())
			{
				var data = ctx.DeliveryRegistrations.Single(m => m.Id == id);
				data.CheckerId = this.GetUserName();
				ctx.SaveChanges();
				return Json(new { result = true });
			}
		}
	}
}