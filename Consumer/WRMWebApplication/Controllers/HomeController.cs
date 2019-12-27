using EFRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WRMWebApplication.Models;

namespace WRMWebApplication.Controllers
{
	public class HomeController : Controller
	{
		[HttpPost]
		public ActionResult PostData(List<APIDeliveryRegistration> Data)
		{
			int insertCount = 0,
				updateCount = 0,
				deleteCount = 0;
			List<APIDeliveryRegistration> notInserted = new List<APIDeliveryRegistration>(),
				notUpdated = new List<APIDeliveryRegistration>(),
				notDeleted = new List<APIDeliveryRegistration>();
			using (var ctx = new RAKEntities())
			{
				foreach (var item in Data)
				{
					var test = (EFRepository.DeliveryRegistration)Data[0];
					if (item.Action.StartsWith("I", StringComparison.OrdinalIgnoreCase))
					{
						item.CreationDate = item.LastModifictionDate = DateTime.Now;
						insertCount = InsertDeliveryRegistration(insertCount, notInserted, ctx, item);
					}
					else if (item.Action.StartsWith("M", StringComparison.OrdinalIgnoreCase))
					{
						item.LastModifictionDate = DateTime.Now;
						updateCount = UpdateDeliveryRegistration(updateCount, notUpdated, ctx, item);
					}
					else if (item.Action.StartsWith("D", StringComparison.OrdinalIgnoreCase))
					{
						deleteCount = DeleteDeliveryRegistration(deleteCount, notDeleted, ctx, item);
					}
				}
			}
			return Json(new { insertCount, updateCount, deleteCount, notInserted, notUpdated, notDeleted }, JsonRequestBehavior.AllowGet);
		}

		private static int InsertDeliveryRegistration(int insertCount, List<APIDeliveryRegistration> notInserted, EFRepository.RAKEntities ctx, APIDeliveryRegistration item)
		{
			DeliveryRegistration temp = item.ToBase();
			insertCount++;
			try
			{
				ctx.DeliveryRegistrations.Add(temp as DeliveryRegistration);
				ctx.SaveChanges();
			}
			catch(Exception e)
			{
				notInserted.Add(item);
			}

			return insertCount;
		}

		private static int UpdateDeliveryRegistration(int updateCount, List<APIDeliveryRegistration> notUpdated, EFRepository.RAKEntities ctx, APIDeliveryRegistration item)
		{
			updateCount++;
			try
			{
				var updateItem = ctx.DeliveryRegistrations.Single(m => m.CIF == item.CIF);
				updateItem.UpdateValues(item);
				ctx.SaveChanges();
			}
			catch
			{
				notUpdated.Add(item);
			}

			return updateCount;
		}

		private static int DeleteDeliveryRegistration(int deleteCount, List<APIDeliveryRegistration> notDeleted, EFRepository.RAKEntities ctx, APIDeliveryRegistration item)
		{
			deleteCount++;
			try
			{
				var deleteItem = ctx.DeliveryRegistrations.Single(m => m.CIF == item.CIF);
				deleteItem.UpdateValues(item);
				ctx.DeliveryRegistrations.Remove(deleteItem);
				ctx.SaveChanges();
			}
			catch
			{
				notDeleted.Add(item);
			}

			return deleteCount;
		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult JsonTest()
		{
			var errors = new List<Error>
			{
				new Error(10,"CIF is invalid"),
				new Error(11,"..."),
				new Error(12,"..."),
			};
			return Json(errors, JsonRequestBehavior.AllowGet);
		}
	}
}