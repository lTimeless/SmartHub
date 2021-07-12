using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SmartHub.Application.Common.Interfaces.Database
{
	public interface IBaseRepositoryAsync<T> where T : class
	{
		Task<List<T>> GetAllAsync();
		IQueryable<T> GetAllAsQueryable();

		Task<T> FindByIdAsync(string id);
		IQueryable<T> FindAllBy(Expression<Func<T, bool>> expression);
		Task<T?> FindByAsync(Expression<Func<T, bool>> expression);

		Task<bool> AddAsync(T entity);
		Task<bool> AddRangeAsync(IEnumerable<T> entity);

		Task<bool> RemoveAsync(T entity);
		Task<bool> RemoveRangeAsync(IEnumerable<T> entities);
	}
}