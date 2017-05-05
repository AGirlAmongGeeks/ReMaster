using ReMaster.BusinessLogic.Company.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RModel = ReMaster.EntityFramework.Model;

namespace ReMaster.BusinessLogic
{
    public interface IProjectMetaDataService
	{
		DateTime GetDateOfLastImport(ProviderType pt);
		void SetDateOfLastImport(ProviderType pType, DateTime date);
	}
}
