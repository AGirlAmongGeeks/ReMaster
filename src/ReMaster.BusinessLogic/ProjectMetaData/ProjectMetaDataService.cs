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

namespace ReMaster.BusinessLogic
{
    public class ProjectMetaDataService : IProjectMetaDataService
	{
		IProjectMetaDataRepository<RCModel.ProjectMetaData> metaDataRepoExtended;
		IRepositoryBase<RCModel.ProjectMetaData> metaDataRepoBase;
		
		public ProjectMetaDataService(IRepositoryBase<RCModel.ProjectMetaData> _mdRepo, IProjectMetaDataRepository<RCModel.ProjectMetaData> _mdRepo2)
		{
			this.metaDataRepoExtended = _mdRepo2;
			this.metaDataRepoBase = _mdRepo;
		}

		#region GetDateOfLastImport()

		public DateTime GetDateOfLastImport(ProviderType pType)
		{
			var result = DateTime.MinValue;
			var metaDataKey = "";
			switch (pType)
			{
				case ProviderType.CEIDG:
					metaDataKey = ConstKeys.DateLastImportPL;
					break;
					//case ....:
			}

			var dateOfLastImport = metaDataRepoExtended.GetValue(metaDataKey);
			if (String.IsNullOrEmpty(dateOfLastImport))
			{
				metaDataRepoExtended.SaveValue(metaDataKey, result);
			}
			else
			{
				DateTime.TryParse(dateOfLastImport, out result);
			}

			return result;
		}

		#endregion

		#region SetDateOfLastImport()

		public void SetDateOfLastImport(ProviderType pType, DateTime date)
		{
			var metaDataKey = "";
			switch (pType)
			{
				case ProviderType.CEIDG:
					metaDataKey = ConstKeys.DateLastImportPL;
					break;
					//case ....:
			}

			metaDataRepoExtended.SaveValue(metaDataKey, date);
		} 

		#endregion

	}
}
