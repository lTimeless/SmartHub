using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartHub.Api.Installers;
using System;
using System.Linq;

namespace SmartHub.Api.Extensions
{
	public static class ServiceExtension
	{
		public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
		{
			var installers = typeof(Startup).Assembly.ExportedTypes.Where(x =>
					typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
				.Select(Activator.CreateInstance).Cast<IInstaller>().ToList();

			installers.ForEach(x => x.InstallServices(services, configuration));
		}
	}
}
