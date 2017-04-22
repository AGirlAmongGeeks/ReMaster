using Microsoft.AspNetCore.Mvc;
using ReMaster.BusinessLogic.Company;
using ReMaster.Core.Providers.CEIDG;

namespace ReMaster.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("admin/[controller]")]
	public class CompanyController : Controller
    {
		private readonly ICompanyAppService companyService;
	
		public CompanyController(ICompanyAppService _companyService)
		{
			this.companyService = _companyService;
		}

		public IActionResult Index()
        {
			return View();
        }

		[Route("ImportCeidg")]
		public IActionResult ImportCeidg()
	    {
			CEIDGProvider.ImportClients(companyService);
		    return View("Import");
	    }
    }
}
