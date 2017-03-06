using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReMaster.Utilities.Data
{
    public interface IBaseDataContext
    {
		DbSet<T> Set<T>() where T : class;
		int SaveChanges();
		void ExecuteCommand(string command, params object[] parameters);
	}
}
