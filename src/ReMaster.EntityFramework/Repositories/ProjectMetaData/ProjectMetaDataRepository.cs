using Microsoft.EntityFrameworkCore;
using NLog.Fluent;
using ReMaster.Utilities.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReMaster.EntityFramework.Repositories
{
	public class ProjectMetaDataRepository<T> : IProjectMetaDataRepository<T> where T : class
	{
		//TODO: Read ProjectMetaData values to the cache.

		private ReMasterDbContext dataContext;
		public readonly DbSet<T> dbSet;

		public ProjectMetaDataRepository(ReMasterDbContext _dataContext)
		{
			dataContext = _dataContext;
			dbSet = dataContext.Set<T>();
		}
		static object _lock = new object();

		#region GetValue()

		public string GetValue(string key)
		{
			try
			{
				lock (_lock)
				{
					var item = dataContext.ProjectMetaDatas.FirstOrDefault(c => c.Key == key);
					if (item != null)
					{
						return item.Value;
					}

					return String.Empty;
				}
			}
			catch (Exception ex)
			{
				ErrorLog.Save(ex);
				return String.Empty;
			}
		} 

		#endregion

		#region SaveValue()

		public void SaveValue(string key, object value)
		{
			try
			{
				lock (_lock)
				{
					var item = dataContext.ProjectMetaDatas.FirstOrDefault(c => c.Key == key);
					if (item != null)
					{
						item.Value = value.ToString();
					}
					else
					{
						item = new Model.ProjectMetaData() { Key = key, Value = value.ToString() };
						dataContext.ProjectMetaDatas.Add(item);
					}

					dataContext.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				ErrorLog.Save(ex);
				throw;
			}
		} 

		#endregion
	}
}
