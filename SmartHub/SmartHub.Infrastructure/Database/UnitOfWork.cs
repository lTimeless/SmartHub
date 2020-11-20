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
		private readonly IChannelManager _channelManager;

		public AppDbContext AppDbContext { get; }

		public UnitOfWork(AppDbContext appDbContext, IChannelManager channelManager)
		{
			AppDbContext = appDbContext;
			_channelManager = channelManager;
		}

		public async Task SaveAsync()
		{
			// TODO: rewrite, damit dennoch alle domainevents ausgeführt werden
			//var aggregateRoots = AppDbContext.ChangeTracker.Entries().Where(x => x.Entity is IAggregateRoot)
			//	.Select(x => x.Entity as IAggregateRoot).ToList();

			await AppDbContext.SaveChangesAsync().ConfigureAwait(false);

			//foreach (var item in aggregateRoots.Where(item => item?.Events != null))
			//{
			//	if (item == null)
			//	{
			//		continue;
			//	}
			//	foreach (var itemEvent in item.Events)
			//	{
			//		await _channelManager.PublishNextToChannel(ChannelNames.System, itemEvent).ConfigureAwait(false);
			//	}
			//	item.ClearDomainEvents();
			//}
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
