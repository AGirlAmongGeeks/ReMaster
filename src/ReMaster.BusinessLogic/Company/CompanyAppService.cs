using AutoMapper;
using ReMaster.BusinessLogic.Company.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RCModel = ReMaster.EntityFramework.Model;
using ReMaster.EntityFramework.Repositories;
//using ReMaster.BusinessLogic.UnitOfWork;
using System.Linq;

namespace ReMaster.BusinessLogic.Company
{
    public class CompanyAppService : ICompanyAppService
    {
		ICompanyRepository<RCModel.Company> companyRepoExtended;
		IRepositoryBase<RCModel.Company> companyRepoBase;
		
		public CompanyAppService(IRepositoryBase<RCModel.Company> _companyRepo, ICompanyRepository<RCModel.Company> _companyRepo2)
		{
			this.companyRepoExtended = _companyRepo2;
			this.companyRepoBase = _companyRepo;
		}

		#region GetCompanies()
		public List<CompanyListDto> GetCompanies()
		{
			var companies = companyRepoBase.GetAll();

			return Mapper.Map<IEnumerable<RCModel.Company>, List<CompanyListDto>>(companies);
		}
		#endregion

		#region GetById()
		public CompanyListDto GetById(int id)
		{
			var companies = companyRepoBase.GetById(id);

			return Mapper.Map<RCModel.Company, CompanyListDto>(companies);
		}
		#endregion

		#region AddCompanies()
		public void AddCompanies(List<RCModel.Company> items, bool isInitialImport)
		{
			if (isInitialImport)
			{
				companyRepoBase.AddAll(items);
			}
			else
			{
				companyRepoExtended.AddAndDeleteNotPresent(items);
			}
		}
		#endregion
		
	}
}
