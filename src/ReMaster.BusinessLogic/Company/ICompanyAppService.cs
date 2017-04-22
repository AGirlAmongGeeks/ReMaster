using ReMaster.BusinessLogic.Company.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RModel = ReMaster.EntityFramework.Model;

namespace ReMaster.BusinessLogic.Company
{
    public interface ICompanyAppService
    {
		List<CompanyListDto> GetCompanies();
		void AddCompanies(List<RModel.Company> items);
		CompanyListDto GetById(int id);
	}
}
