using Microsoft.EntityFrameworkCore;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartHub.Application.Common.Interfaces.Repositories;
using SmartHub.Domain.Common.Enums;

namespace SmartHub.Infrastructure.Database.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private IHomeRepository _homeRepository;
		private IUserRepository _userRepository;
		private readonly IChannelManager _channelManager;
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<Role> _roleManager;

		public AppDbContext AppDbContext { get; }

		public UnitOfWork(AppDbContext appDbContext, IChannelManager channelManager,
			UserManager<User> userManager, RoleManager<Role> roleManager)
		{
			AppDbContext = appDbContext;
			_channelManager = channelManager;
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public IHomeRepository HomeRepository => _homeRepository ??= new HomeRepositoryAsync(AppDbContext);
		public IUserRepository UserRepository => _userRepository ??= new UserRepository(_userManager, _roleManager);

		public async Task SaveAsync()
		{
			var aggregateRoots = AppDbContext.ChangeTracker.Entries().Where(x => x.Entity is IAggregateRoot)
				.Select(x => x.Entity as IAggregateRoot).ToList();

			await AppDbContext.SaveChangesAsync();

			foreach (var item in aggregateRoots.Where(item => item?.Events != null))
			{
				if (item == null) continue;
				foreach (var itemEvent in item.Events)
				{
					await _channelManager.PublishNextToChannel(EventTypes.All, itemEvent).ConfigureAwait(false);
				}
				item.ClearDomainEvents();
			}
		}

		public void Dispose()
		{
			AppDbContext.Dispose();
		}

		public async Task RollbackAsync()
		{
			AppDbContext.ChangeTracker.Entries()
				.Where(e => e.Entity != null) // vlt das hinzufügen => && (e.State == EntityState.Added || e.State == EntityState.Modified)
				.ToList()
				.ForEach(e => e.State = EntityState.Detached);
			await SaveAsync();
		}
	}
}
