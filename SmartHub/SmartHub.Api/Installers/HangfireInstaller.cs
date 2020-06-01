using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SmartHub.Api.Installers
{
	public class HangfireInstaller : IInstaller
	{
		public void InstallServices(IServiceCollection services, IConfiguration configuration)
		{
			services.AddHangfire(x => x.UsePostgreSqlStorage(configuration.GetConnectionString("sqlConnection")));
			services.AddHangfireServer();
		}
	}
}
