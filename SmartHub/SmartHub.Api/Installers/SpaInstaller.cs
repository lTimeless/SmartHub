using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SmartHub.Api.Installers
{
	public class SpaInstaller : IInstaller
	{
		public void InstallServices(IServiceCollection services, IConfiguration configuration)
		{
			// In production, the files will be served from this directory
			services.AddSpaStaticFiles(options =>
			{
				options.RootPath = "wwwroot";
			});
		}
	}
}
