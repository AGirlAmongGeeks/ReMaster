using Microsoft.AspNetCore.Mvc;
using ReMaster.BusinessLogic;
using ReMaster.BusinessLogic.Company;
using ReMaster.Core.Providers.CEIDG;

namespace ReMaster.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("admin/[controller]")]
	public class CompanyController : Controller
    {
		private readonly ICompanyAppService companyService;
		private readonly IProjectMetaDataService metaDataService;
	
		public CompanyController(ICompanyAppService _companyService, IProjectMetaDataService _metaDataService)
		{
			this.companyService = _companyService;
			this.metaDataService = _metaDataService;
		}

		public IActionResult Index()
        {
			return View();
        }

		[Route("ImportCeidg")]
		public IActionResult ImportCeidg()
	    {
			CEIDGProvider.ImportClients(companyService, metaDataService);
		    return View("Import");
	    }
    }
}
