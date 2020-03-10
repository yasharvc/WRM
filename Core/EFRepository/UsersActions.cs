using System.Collections.Generic;
using System.Linq;

namespace EFRepository
{
	public class UsersActions
	{
		public IEnumerable<UserWithGroup> GetAllUserWithGroup()
		{
			using(var ctx = new RAKEntities())
			{
				return (from u in ctx.Users
						   join ug in ctx.UserGroups on u.Id equals ug.UserId
						   join gr in ctx.Groups on ug.GroupId equals gr.Id
						   select new UserWithGroup
						   {
							   User = u,
							   Group = gr
						   }).ToList();
			}
		}

		public void CreateUser(string username,string password,GroupType group)
		{
			using(var ctx = new RAKEntities())
			{
				if (string.IsNullOrEmpty(username))
					throw new System.Exception("User name is empty");
				if (string.IsNullOrEmpty(password))
					throw new System.Exception("Password is empty");
				if((int)group < 0)
					throw new System.Exception("Group is empty");
				var user = new User
				{
					Password = password,
					Username = username
				};
				if (ctx.Users.Any(m => m.Username == username))
					throw new System.Exception("User name is already exists");
				ctx.Users.Add(user);
				ctx.SaveChanges();
				ctx.UserGroups.Add(new UserGroup { UserId = user.Id, GroupId = (int)group });
				ctx.SaveChanges();
			}
		}
	}
}