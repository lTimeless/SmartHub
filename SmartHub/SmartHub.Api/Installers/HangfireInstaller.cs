using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartHub.Application.Common.Utils;

namespace SmartHub.Api.Installers
{
	public class HangfireInstaller : IInstaller
	{
		public void InstallServices(IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = StringUtils.CreateConnectionString(configuration);
			services.AddHangfire(x => x.UsePostgreSqlStorage(connectionString));
			services.AddHangfireServer();
		}
	}
}
