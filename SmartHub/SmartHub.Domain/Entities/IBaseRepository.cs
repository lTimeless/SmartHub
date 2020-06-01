using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SmartHub.Domain.Entities
{
	public interface IBaseRepository<T> where T : BaseEntity
	{
		Task<T> GetByIdAsync(string id);

		Task<List<T>> GetAllAsync();

		Task<T> GetFirstAsync();

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
