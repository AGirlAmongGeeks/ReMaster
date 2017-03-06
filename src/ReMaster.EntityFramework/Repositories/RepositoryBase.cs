using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using ReMaster.EntityFramework;
using Microsoft.Extensions.Logging;
using ReMaster.Utilities.Tools;

namespace ReMaster.EntityFramework.Repositories
{
	public class RepositoryBase<T> : IRepositoryBase<T> where T : class
	{
		private ReMasterDbContext _dataContext;
		public readonly DbSet<T> dbSet;
		
		protected RepositoryBase(ReMasterDbContext dataContext)
		{
			_dataContext = dataContext;
			dbSet = dataContext.Set<T>();
		}

		public virtual void Add(T entity)
		{
			try
			{
				dbSet.Add(entity);
			}
			catch (Exception ex)
			{
				ErrorLog.Save(ex);
				throw;
			}
		}

		public virtual T Update(T entity)
		{
			try
			{
				_dataContext.Entry(entity).State = EntityState.Modified;
				_dataContext.SaveChanges();
				return entity;
			}
			catch (Exception e)
			{
				ErrorLog.Save(e);
				throw;
			}
		}

		public virtual void Delete(T entity)
		{
			try
			{
				dbSet.Remove(entity);
				_dataContext.SaveChanges();
			}
			catch (Exception e)
			{
				ErrorLog.Save(e);
				throw;
			}
		}

		public virtual void Delete(Func<T, bool> where)
		{
			try
			{
				var objects = dbSet.Where(where).AsEnumerable();
				foreach (var obj in objects)
				{
					dbSet.Remove(obj);
				}
				_dataContext.SaveChanges();
			}
			catch (Exception e)
			{
				ErrorLog.Save(e);
				throw;
			}
		}

		public virtual void DeleteById(int id)
		{
			try
			{
				var entity = GetById(id);
				if (entity == null) return;
				dbSet.Remove(entity);
				_dataContext.SaveChanges();
			}
			catch (Exception e)
			{
				ErrorLog.Save(e);
				throw;
			}
		}

		public virtual T GetById(int id)
		{
			try
			{
				return dbSet.Find(id);
			}
			catch (Exception e)
			{
				ErrorLog.Save(e);
				throw;
			}
		}

		public virtual T Get(Func<T, bool> where)
		{
			try
			{
				return dbSet.Where(where).FirstOrDefault();
			}
			catch (Exception e)
			{
				ErrorLog.Save(e);
				throw;
			}
		}

		public virtual int GetCount()
		{
			try
			{
				return dbSet.Count();
			}
			catch (Exception e)
			{
				ErrorLog.Save(e);
				throw;
			}
		}

		public virtual int GetCount(Func<T, bool> where)
		{
			try
			{
				return dbSet.Count(where);
			}
			catch (Exception e)
			{
				ErrorLog.Save(e);
				throw;
			}
		}

		public virtual IEnumerable<T> GetAll()
		{
			try
			{
				return dbSet.ToList();
			}
			catch (Exception e)
			{
				ErrorLog.Save(e);
				throw;
			}
		}

		public virtual IEnumerable<T> GetMany(Func<T, bool> where)
		{
			try
			{
				return dbSet.Where(where).ToList();
			}
			catch (Exception e)
			{
				ErrorLog.Save(e);
				throw;
			}
		}

		public void ExecuteCommand(string sql, params object[] parameters)
		{
			try
			{
				_dataContext.Database.ExecuteSqlCommand(sql, parameters);
			}
			catch (Exception e)
			{
				ErrorLog.Save(e);
				throw;
			}
		}
	}
}
