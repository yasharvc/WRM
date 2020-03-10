using EFRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WRMWebApplication.Controllers
{
	public class CheckerController : Controller
	{
		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			try
			{
				if (this.GetUserGroup().Id != (int)GroupType.Checker)
				{
					filterContext.Result = new RedirectToRouteResult(
						new RouteValueDictionary {
				{ "Controller", "Home" },
				{ "Action", "Index" }
						});
				}
			}
			catch
			{
				filterContext.Result = new RedirectToRouteResult(
						new RouteValueDictionary {
				{ "Controller", "Home" },
				{ "Action", "Index" }
						});
			}
			base.OnActionExecuting(filterContext);
		}
		// GET: Checker
		public ActionResult Index()
		{
			return View();
		}

		public int PageCount()
		{
			return 2;
		}
		public int RecordsInPage()
		{
			return 10;
		}
		public JsonResult GetPageData(int id)
		{
			List<object> res = new List<object>();
			for (var i = 0; i < RecordsInPage(); i++)
			{
				res.Add(new { name = $"Name {i + 1} in page {id}", age = i * 10, tool = "" });
			}
			return Json(res, JsonRequestBehavior.AllowGet);
		}

		public JsonResult GetColumnNames()
		{
			return Json(new string[] { "name", "age", "tool" }, JsonRequestBehavior.AllowGet);
		}

		public JsonResult GetColumnTitles()
		{
			return Json(new string[] { "User name", "Age", "Actions" }, JsonRequestBehavior.AllowGet);
		}
	}
}