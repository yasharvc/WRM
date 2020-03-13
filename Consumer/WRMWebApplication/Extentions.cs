using EFRepository;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WRMWebApplication
{
	public static class Extentions
	{
		public static List<string> GetGroups(this Controller controller)
		{
			return controller.Session["groups"] as List<string>;
		}

		public static bool IsLoggedIn(this Controller controller)
		{
			try
			{
				return GetGroups(controller).Count > 0;
			}
			catch
			{
				return false;
			}
		}

		public static IEnumerable<GoupMenu> GetUserMenus(this Controller controller)
		{
			return controller.Session["Menu"] as IEnumerable<GoupMenu>;
		}

		public static UserGroup GetUserGroup(this Controller controller)
		{
			return controller.Session["group"] as UserGroup;
		}

		public static string GetUserName(this Controller controller)
		{
			return Environment.UserName;
		}
	}
}