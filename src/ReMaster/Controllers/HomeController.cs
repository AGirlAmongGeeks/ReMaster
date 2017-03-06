using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReMaster.Utilities.Tools;

namespace ReMaster.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
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
