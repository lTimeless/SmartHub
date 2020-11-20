using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.Common.Interfaces.Database
{
	public interface IBaseRepositoryAsync<T> where T : BaseEntity
	{
		Task<T> GetByIdAsync(string id);

		Task<List<T>> GetAllAsync();

		Task<List<T>> FindAllAsync(Expression<Func<T, bool>> expression);

		Task<T> FindbyAsync(Expression<Func<T, bool>> expression);

		Task<bool> AddAsync(T entity);

		Task<bool> AddRangeAsync(IEnumerable<T> entity);

		Task<bool> UpdateAsync(T entity);

		Task<bool> UpdateRangeAsync(IEnumerable<T> entity);

		Task<bool> RemoveAsync(T entity);

		Task<bool> RemoveRangeAsync(IEnumerable<T> entities);
	}
}
