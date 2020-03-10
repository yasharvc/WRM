using EFRepository;
using System.Web.Mvc;

namespace WRMWebApplication
{
	public static class Extentions
	{
		public static User GetUser(this Controller controller)
		{
			return controller.Session["user"] as User;
		}
		public static UserGroup GetUserGroup(this Controller controller)
		{
			return controller.Session["group"] as UserGroup;
		}
	}
}