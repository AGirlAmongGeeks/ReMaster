using ReMaster.BusinessLogic.Company.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RModel = ReMaster.EntityFramework.Model;
using ReMaster.Utilities;

namespace ReMaster.BusinessLogic.Company
{
    public interface ICompanyAppService
    {
		List<CompanyListDto> GetAll();
		void AddCompanies(List<RModel.Company> items, bool isInitialImport);
		CompanyListDto GetById(int id);
        List<CompanyListDto> GetList(Pager pager);
    }
}
