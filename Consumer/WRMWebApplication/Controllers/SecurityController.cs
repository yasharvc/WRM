using EFRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Security;

namespace WRMWebApplication.Controllers
{
	public class SecurityController : Controller
	{
		public ActionResult Login()
		{
			try
			{
				if (ConfigurationManager.AppSettings["GOD"] == "1")
				{
					Session["groups"] = new List<string>
					{
						"CCMWRM_Admin"
					};

					Session["Menu"] = new List<GoupMenu>
					{
						new GoupMenu
						{
							Link = "/Checker/Index",
							Title="Checker list",
							GroupID=3,
							Id=1
						},
						new GoupMenu
						{
							Link = "/Registration/New",
							Title="New registration",
							GroupID=2,
							Id=2
						}
					};
				}
				else
				{
					if ((new SecurityActions().GetUserMenu(GetGroups()) as List<GoupMenu>).Count == 0)
						throw new Exception();
					Session["groups"] = GetGroups();
					Session["Menu"] = new SecurityActions().GetUserMenu(GetGroups());
				}
				FormsAuthentication.SetAuthCookie(Environment.UserName, false);
				return RedirectToAction("Index", "Home");
			}
			catch (Exception e)
			{
				var ex = e.InnerException;
				return Content(e.Message + " --- <br/>" + (ex != null ? ex.Message : ""));
			}
		}

		[HttpGet]
		public ActionResult Logout()
		{
			FormsAuthentication.SignOut();
			return RedirectToAction("Login");
		}


		bool AuthenticateUser()
		{
			return false;
		}

		private List<string> GetGroups()
		{
			List<string> result = new List<string>();
			var x = HttpContext.Request.LogonUserIdentity.Groups;
			foreach(var grp in x)
			{
				try
				{
					result.Add(grp.Translate(typeof(NTAccount)).ToString().Split('\\')[1]);
				}
				catch
				{
					result.Add(grp.Translate(typeof(NTAccount)).ToString());
				}
			}
			result.Sort();
			var query = "\"" + string.Join("\",\"", result) + "\"";
			return result;
		}
	}
}