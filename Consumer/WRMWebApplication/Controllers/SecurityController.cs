using System;
using System.Web.Mvc;
using System.Web.Security;
using EFRepository;

namespace WRMWebApplication.Controllers
{
	public class SecurityController : Controller
    {
		public ActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public ActionResult Login(string username, string password)
		{
			try
			{
				var user = new SecurityActions().GetByUsernamePassword(username, password);
				if (user != null)
				{
					user.Password = "****";
					Session["user"] = user;
					Session["group"] = new SecurityActions().GetUserGroup(user.Id);
					FormsAuthentication.SetAuthCookie(user.Username, false);
					return Json(new { result = true, message = "" });
				}
				throw new Exception();
			}
			catch (Exception e)
			{
				var ex = e.InnerException;
				return Json(new { result = false, message = "Username and/or password is not correct" });
			}
		}
		[HttpGet]
		public ActionResult Logout()
		{
			FormsAuthentication.SignOut();
			return RedirectToAction("Login");
		}
	}
}