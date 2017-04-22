using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ReMaster.Utilities.Tools;
using ReMaster.BusinessLogic.Company;

namespace ReMaster.Controllers
{
    public class HomeController : Controller
    {
		public IActionResult Index()
        {
			//var test = ConfigurationHelper.Get(Directory.GetCurrentDirectory(), null, true);
			//var aa = test.GetValue<string>("CEIDGApiKey");
			//ErrorLog.Save(new Exception("testowy", null));
			return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
