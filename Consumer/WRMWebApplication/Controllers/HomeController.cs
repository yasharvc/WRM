using EFRepository;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WRMWebApplication.Models;
using WRMWebApplication;
using System.Web.Routing;

namespace WRMWebApplication.Controllers
{
	[Authorize]
	public class HomeController : BaseController
	{
		static Logger logger = LogManager.GetCurrentClassLogger();
		static Configuration Configuration { get; set; }
		static List<string> FileNames { get; set; }
		static bool isInProcess = false;

		public HomeController()
		{
			if(Configuration == null)
				LoadConfiguration();
		}

		public ContentResult LoadConfiguration()
		{
			using (RAKEntities ctx = new RAKEntities())
			{
				var url = ctx.Paths.FirstOrDefault();
				Configuration = new Configuration
				{
					UploadPath = url.URL
				};
			}
			if (FileNames == null)
				FileNames = new List<string>();
			return Content("1");
		}

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

		private static int InsertDeliveryRegistration(int insertCount, List<APIDeliveryRegistration> notInserted, RAKEntities ctx, APIDeliveryRegistration item)
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

		private static int UpdateDeliveryRegistration(int updateCount, List<APIDeliveryRegistration> notUpdated, RAKEntities ctx, APIDeliveryRegistration item)
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

		private static int DeleteDeliveryRegistration(int deleteCount, List<APIDeliveryRegistration> notDeleted, RAKEntities ctx, APIDeliveryRegistration item)
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
			return View(this.GetUserMenus());
		}

		public ActionResult JsonTest()
		{
			return Json(new RAKEntities().DeliveryRegistrations.Take(2), JsonRequestBehavior.AllowGet);
		}
		public async Task<JsonResult> ProcessUploadFiles()
		{
			if (isInProcess)
				return Json(new { result = false, message = "Processor is busy" }, JsonRequestBehavior.AllowGet);
			isInProcess = true;
			await ProcessDirectory();
			return Json(new { result = true, message = "" }, JsonRequestBehavior.AllowGet);
		}

		private async Task ProcessDirectory()
		{
			await Task.Factory.StartNew(() =>
			{
				if (ConfigurationPathIsValid())
				{
					var lst = Directory.GetFiles(Configuration.UploadPath).Except(FileNames);
					foreach (var filePath in lst)
					{
						try
						{
							var processor = new FileProcessor(filePath);
							processor.OnProcessResult += (obj, res) =>
							{
								if (res == ProcessResult.Processed)
									logger.Info(string.Format("File '{0}' processed successfully", (obj as FileProcessor).PathToFile));
								else if (res == ProcessResult.Corrupted)
									logger.Error(string.Format("File '{0}' corrupted!!!", (obj as FileProcessor).PathToFile));
								else if (res == ProcessResult.AlreadyProcessed)
									logger.Info(string.Format("File '{0}' already processed", (obj as FileProcessor).PathToFile));
							};
							processor.Process();
						}
						catch (DbEntityValidationException e)
						{
							foreach (var eve in e.EntityValidationErrors)
							{
								var error = (string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
									eve.Entry.Entity.GetType().Name, eve.Entry.State));
								foreach (var ve in eve.ValidationErrors)
								{
									error += "\r\n\t" + string.Format("- Property: \"{0}\", Error: \"{1}\"",
										ve.PropertyName, ve.ErrorMessage);
								}
								logger.ErrorException(error, e);
							}
						}
						catch (EntityCommandExecutionException entityEx)
						{
							logger.ErrorException(entityEx.InnerException.Message, entityEx);
						}
						catch (Exception ex)
						{
							logger.ErrorException("Unexpected error!!!!", ex);
						}
						FileNames.Add(filePath);
					}
				}
				isInProcess = false;
				FileNames = new List<string>();
			});
		}

		private bool ConfigurationPathIsValid()
		{
			return !(string.IsNullOrEmpty(Configuration.UploadPath) || string.IsNullOrWhiteSpace(Configuration.UploadPath));
		}
	}
}