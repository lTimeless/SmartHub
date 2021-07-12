using Microsoft.EntityFrameworkCore;
using SmartHub.Application.Common.Interfaces.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SmartHub.Infrastructure.Database.Repositories
{
	public class BaseRepositoryAsync<T> : IBaseRepositoryAsync<T> where T : class
	{
		private readonly DbSet<T> _entities;

		public BaseRepositoryAsync(AppDbContext appDbContext)
		{
			_entities = appDbContext.Set<T>();
		}

		#region Get

		public async Task<List<T>> GetAllAsync()
		{
			return await _entities.ToListAsync();
		}

		public IQueryable<T> GetAllAsQueryable()
		{
			return _entities.AsNoTracking();
		}

		#endregion

		#region Find

		public async Task<T> FindByIdAsync(string id)
		{
			return await _entities.FindAsync(id);
		}

		public IQueryable<T> FindAllBy(Expression<Func<T, bool>> expression)
		{
			return _entities.Where(expression);
		}

		public async Task<T?> FindByAsync(Expression<Func<T, bool>> expression)
		{
			return await _entities.Where(expression).FirstOrDefaultAsync();
		}

		#endregion Getter

		#region Add

		public async Task<bool> AddAsync(T entity)
		{
			try
			{
				await _entities.AddAsync(entity);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public async Task<bool> AddRangeAsync(IEnumerable<T> entities)
		{
			try
			{
				await _entities.AddRangeAsync(entities);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		#endregion Add

		#region Remove

		public async Task<bool> RemoveAsync(T entity)
		{
			try
			{
				_entities.Remove(entity);
				return await Task.FromResult(true).ConfigureAwait(false);
			}
			catch (Exception)
			{
				return await Task.FromResult(false).ConfigureAwait(false);
			}
		}

		public async Task<bool> RemoveRangeAsync(IEnumerable<T> entities)
		{
			try
			{
				_entities.RemoveRange(entities);
				return await Task.FromResult(true).ConfigureAwait(false);
			}
			catch (Exception)
			{
				return await Task.FromResult(false).ConfigureAwait(false);
			}
		}

		#endregion Remove
	}
}