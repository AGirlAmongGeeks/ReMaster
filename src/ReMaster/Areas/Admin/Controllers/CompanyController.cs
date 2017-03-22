using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ReMaster.Core.Providers.CEIDG;
using ReMaster.Utilities.Tools;

namespace ReMaster.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("admin/[controller]")]
	public class CompanyController : Controller
    {
        public IActionResult Index()
        {
			return View();
        }

		[Route("ImportCeidg")]
		public IActionResult ImportCeidg()
	    {
			CEIDGProvider.ImportClients();
		    return View("Import");
	    }
    }
}
