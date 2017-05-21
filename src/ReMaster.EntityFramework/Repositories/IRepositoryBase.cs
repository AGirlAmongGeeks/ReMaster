using ReMaster.Utilities;
using ReMaster.Utilities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ReMaster.EntityFramework.Repositories
{
	public interface IRepositoryBase<T> where T : class
	{
		void Add(T entity);
		void AddAll(List<T> entities);
		T Update(T entity);

		void Delete(T entity);
		void Delete(Func<T, bool> predicate);
		void DeleteById(int id);

		T GetById(int id);
		T Get(Func<T, bool> where);

		int GetCount();
		int GetCount(Func<T, bool> where);

		IEnumerable<T> GetAll();
		IQueryable<T> GetTable();
		IEnumerable<T> GetAll(Func<T, string> orderBy);
		IEnumerable<T> GetMany(Func<T, bool> where);
		IEnumerable<T> GetMany(Pager pager);


		void ExecuteCommand(string sql, params object[] parameters);
	}
}
