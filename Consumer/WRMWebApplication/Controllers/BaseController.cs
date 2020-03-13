using System.Web.Mvc;
using System.Web.Routing;

namespace WRMWebApplication.Controllers
{
	public class BaseController : Controller
	{
		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			try
			{
				if (!this.IsLoggedIn())
				{
					filterContext.Result = new RedirectToRouteResult(
						new RouteValueDictionary {
				{ "Controller", "Security" },
				{ "Action", "Login" }
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
	}
}