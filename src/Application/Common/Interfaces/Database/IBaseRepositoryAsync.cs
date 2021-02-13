using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SmartHub.Domain.Entities;
using System.Linq;

namespace SmartHub.Application.Common.Interfaces.Database
{
	public interface IBaseRepositoryAsync<T> where T : BaseEntity
	{
		Task<List<T>> GetAllAsync();
		IQueryable<T> GetAllAsQueryable();

		Task<T> FindByIdAsync(string id);
		IQueryable<T> FindAllAsync(Expression<Func<T, bool>> expression);
		Task<T> FindByAsync(Expression<Func<T, bool>> expression);

		Task<bool> AddAsync(T entity);
		Task<bool> AddRangeAsync(IEnumerable<T> entity);

		Task<bool> RemoveAsync(T entity);
		Task<bool> RemoveRangeAsync(IEnumerable<T> entities);
	}
}
