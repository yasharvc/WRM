using EFRepository;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace WRMWebApplication.Controllers
{
	[Authorize]
    public class AdminController : BaseController
    {
		
		public ActionResult Users()
        {
            return View(new UsersActions().GetAllUserWithGroup());
        }

		[HttpPost]
		public JsonResult Create(string username,string password,int group)
		{
			try
			{
				new UsersActions().CreateUser(username, password, (GroupType)group);
				return Json(new { result = true });
			}catch(Exception e)
			{
				return Json(new { result = false, error = e.Message.Length > 0 ? e.Message : "Input data is invalid" });
			}
		}

		[HttpGet]
		public ActionResult SetMaker(int id)
		{
			return ChangeUserGroup(id, GroupType.Maker);
		}
		[HttpGet]
		public ActionResult SetAdmin(int id)
		{
			return ChangeUserGroup(id, GroupType.Admin);
		}
		[HttpGet]
		public ActionResult SetChecker(int id)
		{
			return ChangeUserGroup(id, GroupType.Checker);
		}

		private ActionResult ChangeUserGroup(int id, GroupType newType)
		{
			using (var ctx = new RAKEntities())
			{
				var userGroup = ctx.UserGroups.Single(m => m.UserId == id);
				userGroup.GroupId = (int)newType;
				ctx.SaveChanges();
				return RedirectToAction("Users");
			}
		}
	}
}