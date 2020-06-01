using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartHub.Domain.Entities.Roles;
using SmartHub.Domain.Entities.Users;
using SmartHub.Infrastructure.Database;

namespace SmartHub.Api.Installers
{
	public class DbInstaller : IInstaller
	{
		public void InstallServices(IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration["ConnectionStrings:sqlConnection"];

			services.AddDbContext<AppDbContext>(builder =>
			{
				builder.EnableSensitiveDataLogging(false);
				builder.UseLazyLoadingProxies();
				builder.UseNpgsql(connectionString,
					options => options.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
			})
			.BuildServiceProvider();

			services.AddIdentity<User, Role>(options =>
			{
				options.SignIn.RequireConfirmedAccount = true;
				options.User.RequireUniqueEmail = false;
			})
				.AddEntityFrameworkStores<AppDbContext>()
				.AddDefaultTokenProviders();

			services.AddIdentityServer()
				.AddApiAuthorization<User, AppDbContext>();
		}
	}
}
