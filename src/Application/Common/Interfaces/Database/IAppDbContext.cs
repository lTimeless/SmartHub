using Microsoft.EntityFrameworkCore;
using SmartHub.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SmartHub.Application.Common.Interfaces.Database
{
	public interface IAppDbContext
	{
		public DbSet<Group> Groups { get; set; }
		public DbSet<Device> Devices { get; set; }
		public DbSet<Plugin> Plugins { get; set; }
		public DbSet<Activity> Activities { get; set; }
		public DbSet<RefreshToken> RefreshTokens { get; set; }
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
	}
}