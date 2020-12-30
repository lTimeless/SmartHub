using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO.Compression;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Polly;
using SmartHub.Api.GraphQl;
using SmartHub.Api.Validators;
using SmartHub.Domain.Common.Settings;
using SmartHub.Domain;

namespace SmartHub.Api.Extensions
{
	public static class ServiceExtension
	{
		public static IServiceCollection AddApiLayer(this IServiceCollection services, IConfiguration configuration)
		{
			// Server configuration
			services.AddServerConfiguration(configuration);
			// GraphQl
			services.AddGraphQl();
			// Controllers
			services.AddControllers();
			// Spa
			services.AddSpaStaticFiles();
			// SignalR
			services.AddSignalR();
			// Http
			services.AddHttpContextAccessor();
			services.AddHttpClientFactory();
			// Response compression
			services.AddResponseCompression();
			return services;
		}

		private static void AddServerConfiguration(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true);

			services.Configure<HostOptions>(configuration.GetSection("HostOptions"));

			// -------------- SmartHubSettings ---------------
			services.TryAddSingleton<IValidateOptions<AppConfig>, ApplicationConfigValidation>();
			services.TryAddSingleton<IValidateOptions<JwtSettings>, JwtSettingsValidation>();
		}

		private static void AddSpaStaticFiles(this IServiceCollection services)
		{
			// In production, the files will be served from this directory
			services.AddSpaStaticFiles(options =>
			{
				options.RootPath = "wwwroot";
			});
		}

		private static void AddGraphQl(this IServiceCollection services)
		{
			services.AddGraphQLServer()
				.AddQueryType<RootQueryType>()
				.AddMutationType<RootMutationType>()
				.AddTypes()
				.AddProjections()
				.AddFiltering()
				.AddSorting()
				;
		}

		private static void AddControllers(this IServiceCollection service)
		{
			service.AddControllers(opt =>
				{
					var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
					opt.Filters.Add(new AuthorizeFilter(policy));
				})
				.SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
		}

		private static void AddResponseCompression(this IServiceCollection services)
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
				options.Providers.Add<GzipCompressionProvider>();
				options.Providers.Add<BrotliCompressionProvider>();
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

		private static void AddHttpClientFactory(this IServiceCollection services)
		{
			services.AddHttpClient("SmartDevices", (x) =>
				{
					x.DefaultRequestHeaders.Add("User-Agent", "smartHub");
				})
				.AddTransientHttpErrorPolicy(x => x.WaitAndRetryAsync(3,
					retryAttempt => TimeSpan.FromMilliseconds(retryAttempt * 100)));
		}

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
