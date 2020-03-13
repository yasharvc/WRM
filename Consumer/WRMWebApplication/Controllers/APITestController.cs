using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WRMWebApplication.Controllers
{
    public class APITestController : Controller
    {	
		//AccNo
		//0180132398004
		//0180173504002
		//CC
		//1531000468912007
		//5171469714906004
        // GET: APITest
        public ActionResult Index1(string accnumber = "0180132398004",string url = "http://10.15.14.219:7080/EAI/ELX/*/EAI/ELX/*")
		{
			url = MakeURL(url);
			var res = new MiddlewareCaller(url).CallWebService(true, accnumber);
			return Content(res);
		}

		private string MakeURL(string url)
		{
			if (string.IsNullOrEmpty(url))
				return "http://10.15.14.219:7080/EAI/ELX/*/EAI/ELX/*";
			else if (url == "CONFIG")
				return ConfigurationManager.AppSettings["URL"];

			url = url.Replace("_", "/").Replace("@", "*");
			if (!url.StartsWith("http://"))
				url = "http://10.15.14.219:7080" + (url.StartsWith("/") ? "" : "/") + url;
			return url;
		}

		public async Task<ActionResult> Index2(string accnumber = "0180132398004", string url = "")
		{
			url = MakeURL(url);
			var res = await new MiddlewareCaller(url).CallWebService2Async(true, accnumber);
			return Content(res);
		}

		public async Task<ActionResult> Index3(string cc = "5171469714906004", string url = "")
		{
			url = MakeURL(url);
			var res = await new MiddlewareCaller(url).CallWebService2Async(false, cc);
			return Content(res);
		}

		public ActionResult Index4(string cc = "5171469714906004", string url = "")
		{
			url = MakeURL(url);
			var res = new MiddlewareCaller(url).CallWebService(false, cc);
			return Content(res);
		}

	}
}