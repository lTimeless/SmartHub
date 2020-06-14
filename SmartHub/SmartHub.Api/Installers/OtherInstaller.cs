using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartHub.Application.UseCases.Entity.Homes;
using System.Reflection;
using SmartHub.Application.Common.Mappings;

namespace SmartHub.Api.Installers
{
	public class OtherInstaller : IInstaller
	{
		public void InstallServices(IServiceCollection services, IConfiguration configuration)
		{
			// AutoMapper
			ConfigureAutoMapper(services);

			// Mediatr
			ConfigureMediatr(services);

			// Http
			//services.AddHttpClient("Sensors", (x) =>
			//{
			//	// x.DefaultRequestHeaders.Add("Accept", "");
			//	x.DefaultRequestHeaders.Add("User-Agent", "smarthome");
			//}).AddTransientHttpErrorPolicy(x =>
			//	x.WaitAndRetryAsync(2, _ => TimeSpan.FromMilliseconds(300)));

			// SignalR
			services.AddSignalR();
		}

		private static void ConfigureMediatr(IServiceCollection services)
		{
			services.AddMediatR(Assembly.Load("SmartHub.Application"));
		}

		private static void ConfigureAutoMapper(IServiceCollection services)
		{
			services.AddAutoMapper(Assembly.Load("SmartHub.Application"));
		}
	}
}
