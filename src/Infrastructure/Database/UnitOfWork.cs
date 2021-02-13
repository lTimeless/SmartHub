using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Interfaces.Database;

namespace SmartHub.Infrastructure.Database
{
	public class UnitOfWork : IUnitOfWork
	{
		public AppDbContext AppDbContext { get; }

		public UnitOfWork(AppDbContext appDbContext)
		{
			AppDbContext = appDbContext;
		}

		public async Task SaveAsync()
		{
			await AppDbContext.SaveChangesAsync();
		}

		public void Dispose()
		{
			AppDbContext.Dispose();
			GC.SuppressFinalize(this);
		}

		public async Task RollbackAsync()
		{
			AppDbContext.ChangeTracker.Entries()
				.Where(e => e.Entity != null) // vlt das hinzufügen => && (e.State == EntityState.Added || e.State == EntityState.Modified)
				.ToList()
				.ForEach(e => e.State = EntityState.Detached);
			await SaveAsync().ConfigureAwait(false);
		}
	}
}
