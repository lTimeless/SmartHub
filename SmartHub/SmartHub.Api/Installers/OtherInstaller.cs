using System;
using System.IO.Compression;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Polly;
using SmartHub.Application.Common.Behaviours;

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

			// Response compression
			ConfigureResponseCompression(services);


			// Http
			services.AddHttpClient("SmartDevices", (x) =>
				{
					x.DefaultRequestHeaders.Add("User-Agent", "smartHub");
				})
				.AddTransientHttpErrorPolicy(x => x.WaitAndRetryAsync(3,
					retryAttempt => TimeSpan.FromMilliseconds(retryAttempt * 100)));

			// SignalR
			services.AddSignalR();
		}

		private static void ConfigureMediatr(IServiceCollection services)
		{
			services.AddMediatR(Assembly.Load("SmartHub.Application"));
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
		}

		private static void ConfigureAutoMapper(IServiceCollection services)
		{
			services.AddAutoMapper(Assembly.Load("SmartHub.Application"));
		}

		private static void ConfigureResponseCompression(IServiceCollection services)
		{
			services.Configure<GzipCompressionProviderOptions>(options =>
			{
				options.Level = CompressionLevel.Fastest;
			});

			services.Configure<BrotliCompressionProviderOptions>(options =>
			{
				options.Level = CompressionLevel.Optimal;
			});
			services.AddResponseCompression(options =>
			{
				options.Providers.Add<BrotliCompressionProvider>();
				options.Providers.Add<GzipCompressionProvider>();
				options.EnableForHttps = true;
				options.MimeTypes = new[]
				{
					// General
					"text/plain",
					"text/html",
					"application/json",
					"application/xml",
					"text/css",
					"text/json",
					"font/woff2",
					"application/javascript",
					"image/x-icon",
					"image/png",
					"image/svg"
				};
			});
		}
	}
}
