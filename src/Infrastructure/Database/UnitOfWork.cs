using Microsoft.EntityFrameworkCore;
using SmartHub.Application.Common.Interfaces.Database;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHub.Infrastructure.Database
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly AppDbContext _appDbContext;

		public UnitOfWork(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}

		public async Task SaveAsync()
		{
			await _appDbContext.SaveChangesAsync();
		}

		public void Dispose()
		{
			_appDbContext.Dispose();
			GC.SuppressFinalize(this);
		}

		public async Task RollbackAsync()
		{
			_appDbContext.ChangeTracker.Entries()
				.Where(e => e.Entity !=
				            null) // vlt das hinzufügen => && (e.State == EntityState.Added || e.State == EntityState.Modified)
				.ToList()
				.ForEach(e => e.State = EntityState.Detached);
			await SaveAsync().ConfigureAwait(false);
		}
	}
}