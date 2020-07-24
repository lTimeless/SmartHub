using System;
using Microsoft.EntityFrameworkCore;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Domain.Entities;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartHub.Application.Common.Interfaces.Repositories;
using SmartHub.Application.Common.Utils;
using SmartHub.Domain.Common.Enums;
using Role = SmartHub.Domain.Entities.Role;

namespace SmartHub.Infrastructure.Database.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private IHomeRepository _homeRepository;
		private IBaseRepository<Group> _groupRepository;
		private IUserRepository _userRepository;
		private readonly IChannelManager _channelManager;
		private readonly IUserAccessor _userAccessor;
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<Role> _roleManager;

		public AppDbContext AppDbContext { get; }

		public UnitOfWork(AppDbContext appDbContext, IChannelManager channelManager, IUserAccessor userAccessor,
			UserManager<User> userManager, RoleManager<Role> roleManager)
		{
			AppDbContext = appDbContext;
			_channelManager = channelManager;
			_userAccessor = userAccessor;
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public IHomeRepository HomeRepository => _homeRepository ??= new HomeRepository(AppDbContext);
		public IBaseRepository<Group> GroupRepository => _groupRepository ??= new BaseRepository<Group>(AppDbContext);
		public IUserRepository UserRepository => _userRepository ??= new UserRepository(_userManager, _roleManager);

		public async Task SaveAsync()
		{
			foreach (var entry in AppDbContext.ChangeTracker.Entries<IEntity>())
			{
				var dateTime = DateTimeUtils.NowUtc;
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
				if (item?.Events == null)
				{
					continue;
				}

				foreach (var itemEvent in item.Events)
				{
					await _channelManager.PublishNextToChannel(EventTypes.All, itemEvent).ConfigureAwait(false);
				}
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
