using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SmartHub.Api.Installers
{
	public class HealthCheckInstaller : IInstaller
	{
		public void InstallServices(IServiceCollection services, IConfiguration configuration)
		{
			//public static void ConfigureHealthChecks( this IServiceCollection services , IConfiguration config )
			//{
			//    var connectionString = config.GetConnectionString("sqlConnection");
			//    services.AddHealthChecks()
			//        .AddDbContextCheck<AppDbContext>()
			//        .AddNpgSql(connectionString)
			//        .AddCheck<ServicesHealthCheck>("All Services")
			//        ;

			//    services.AddSingleton<ServicesHealthCheck>();

			//}
		}
	}
}
