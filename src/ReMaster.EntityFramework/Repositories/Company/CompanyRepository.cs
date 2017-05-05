using Microsoft.EntityFrameworkCore;
using ReMaster.EntityFramework.Model;
using ReMaster.Utilities.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using AutoMapper;

namespace ReMaster.EntityFramework.Repositories
{
	public class CompanyRepository<T> : ICompanyRepository<T> where T : class
	{
		private ReMasterDbContext dataContext;
		public readonly DbSet<T> dbSet;

		public CompanyRepository(ReMasterDbContext _dataContext)
		{
			dataContext = _dataContext;
			dbSet = dataContext.Set<T>();
		}

		#region AddInTransaction()

		public virtual void AddAndDeleteNotPresent(List<Company> entities)
		{
			using (var transaction = dataContext.Database.BeginTransaction(IsolationLevel.ReadCommitted))
			{
				try
				{
					var itemsInDB = dataContext.Companies.ToList();

					foreach (var item in entities)
					{
						var existingItem = itemsInDB.Where(c => c.CeidgId == item.CeidgId).FirstOrDefault();
						if (existingItem != null)
						{
							#region Update.

							var updatingItemId = existingItem.Id;

							Mapper.Map<Company, Company>(item, existingItem);

							existingItem.Id = updatingItemId;

							dataContext.Update(existingItem);

							itemsInDB.Remove(existingItem); 

							#endregion
						}
						else
						{
							#region Insert.

							dataContext.Add(item);

							#endregion
						}
					}

					#region Soft-delete of all the items absent in the current import.

					foreach (var itemToDelete in itemsInDB)
					{
						itemToDelete.IsDeleted = true;
					} 

					#endregion

					dataContext.SaveChanges();

					transaction.Commit();
				}
				catch (Exception ex)
				{
					ErrorLog.Save(ex);
					throw;
				}
			}
		} 

		#endregion
	}
}
