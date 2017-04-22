using Microsoft.EntityFrameworkCore;
using ReMaster.EntityFramework.Model;
using ReMaster.Utilities.Data;
using ReMaster.Utilities.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReMaster.EntityFramework.Repositories
{
	public interface ICompanyRepository<T> where T : class
	{
		void AddInTransaction(List<Company> entity);
	}
}
