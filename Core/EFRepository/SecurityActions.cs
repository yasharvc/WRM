using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRepository
{
	public enum GroupType : int
	{
		None = 0,
		Admin = 1,
		Maker=2,
		Checker=3
	}
	public class SecurityActions
	{
		public bool IsUserNamePasswordExist(string username, string password)
		{
			using (var ctx = new RAKEntities())
			{
				return ctx.Users.Any(m => m.Username == username && m.Password == password);
			}
		}

		public User GetByUsernamePassword(string username, string password)
		{
			using (var ctx = new RAKEntities())
			{
				return ctx.Users.Single(m => m.Username == username && m.Password == password);
			}
		}

		public bool IsAdmin(User user)
		{
			using (var ctx = new RAKEntities())
			{
				return ctx.UserGroups.Any(m => m.UserId == user.Id && m.GroupId == (int)GroupType.Admin);
			}
		}

		public IEnumerable<GoupMenu> GetUserMenu(int userID)
		{
			using(var ctx = new RAKEntities())
			{
				var userGroup = ctx.UserGroups.Single(m => m.UserId == userID);
				return ctx.GoupMenus.Where(m => userGroup.GroupId == m.GroupID).ToList();
			}
		}

		public UserGroup GetUserGroup(int userID)
		{
			using(var ctx = new RAKEntities())
			{
				return ctx.UserGroups.Single(m => m.UserId == userID);
			}
		}
	}
}