using Microsoft.AspNetCore.Mvc;
using ReMaster.BusinessLogic;
using ReMaster.BusinessLogic.Company;
using ReMaster.Core.Providers.CEIDG;
using ReMaster.Utilities;

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

		public IActionResult Index(int pageIndex = 1)
        {
            #region Pager.
            if (pageIndex < 0)
            {
                pageIndex = 1;
            }

            Pager pager = new Pager();
            pager.PageIndex = pageIndex;
            pager.PageSize = 100;
            pager.SortExpression = "Name";
            pager.SortDirection = "DESC";
            #endregion

            var data = companyService.GetList(pager);

            return View(data);
        }

		[Route("ImportCeidg")]
		public IActionResult ImportCeidg()
	    {
			CEIDGProvider.ImportClients(companyService, metaDataService);
		    return View("Import");
	    }
    }
}
