using System;
using Microsoft.EntityFrameworkCore;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Domain.Entities;
using SmartHub.Domain.Entities.Groups;
using SmartHub.Domain.Enums;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using SmartHub.Application.Common.Interfaces.Repositories;

namespace SmartHub.Infrastructure.Database.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private IHomeRepository _homeRepository;
		private IBaseRepository<Group> _groupRepository;
		private readonly IChannelManager _channelManager;
		private readonly IUserAccessor _userAccessor;
		private readonly IDateTimeService _dateTimeService;

		public AppDbContext AppDbContext { get; }

		public UnitOfWork(AppDbContext appDbContext, IChannelManager channelManager, IUserAccessor userAccessor, IDateTimeService dateTimeService)
		{
			AppDbContext = appDbContext;
			_channelManager = channelManager;
			_userAccessor = userAccessor;
			_dateTimeService = dateTimeService;
		}

		public IHomeRepository HomeRepository => _homeRepository ??= new HomeRepository(AppDbContext);
		public IBaseRepository<Group> GroupRepository => _groupRepository ??= new BaseRepository<Group>(AppDbContext);

		public async Task SaveAsync()
		{
			foreach (var entry in AppDbContext.ChangeTracker.Entries<IEntity>())
			{
				var dateTime = _dateTimeService.NowUtc;
				var userName = _userAccessor.GetCurrentUsername();
				switch (entry.State)
				{
					case EntityState.Added:
						entry.Entity.CreatedAt = dateTime;
						entry.Entity.LastModifiedAt = dateTime;
						entry.Entity.CreatedBy = userName;
						entry.Entity.LastModifiedBy = userName;
						break;
					case EntityState.Modified:
						entry.Entity.LastModifiedAt = dateTime;
						entry.Entity.LastModifiedBy = userName;
						break;
					case EntityState.Detached:
						break;
					case EntityState.Unchanged:
						break;
					case EntityState.Deleted:
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}

			var aggregateRoots = AppDbContext.ChangeTracker.Entries().Where(x => x.Entity is IAggregateRoot)
				.Select(x => x.Entity as IAggregateRoot).ToList();

			await AppDbContext.SaveChangesAsync();

			foreach (var item in aggregateRoots)
			{
				await _channelManager.PublishNextToChannel(ChannelEventEnum.Events, item.Events).ConfigureAwait(false);
				item.ClearDomainEvents();
			}
		}

		public void Dispose()
		{
			AppDbContext?.Dispose();
		}

		public async Task Rollback()
		{
			AppDbContext.ChangeTracker.Entries()
				.Where(e => e.Entity != null).ToList() // && e.state == EntityState.Added
				.ForEach(e => e.State = EntityState.Detached);
			await SaveAsync();
		}
	}
}
