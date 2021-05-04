using Boxed.AspNetCore;
using HotChocolate.Execution.Options;
using HotChocolate.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Polly;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Domain.Common.Constants;
using SmartHub.WebUI.GraphQl;
using SmartHub.WebUI.Services;
using System;
using System.IO.Compression;
using SmartHub.Domain.Common.Options;

namespace SmartHub.WebUI.Extensions
{
	public static class ServiceExtension
	{
		public static IServiceCollection AddApiLayer(this IServiceCollection services, IConfiguration configuration)
		{
			// TODO weitermachen bei GraphQLNet5 bei AddCustomRoouting

			services.AddCustomCaching()
				.AddCustomCors()
				.AddCustomOptions(configuration);
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
			// Identity
			services.AddScoped<ICurrentUserService, CurrentUserService>();

			return services;
		}

		/// <summary>
		/// Configures the settings by binding the contents of the appsettings.json file to the specified Plain Old CLR
		/// Objects (POCO) and adding <see cref="IOptions{T}"/> objects to the services collection.
		/// </summary>
		/// <param name="services">The services.</param>
		/// <param name="configuration">The configuration.</param>
		/// <returns>The services with caching services added.</returns>
		private static IServiceCollection AddCustomOptions(this IServiceCollection services,
			IConfiguration configuration) =>
			services
				.ConfigureAndValidateSingleton<ApplicationOptions>(configuration)
				.ConfigureAndValidateSingleton<CacheProfileOptions>(configuration.GetSection(nameof(ApplicationOptions.CacheProfiles)))
				.ConfigureAndValidateSingleton<CompressionOptions>(configuration.GetSection(nameof(ApplicationOptions.Compression)))
				.ConfigureAndValidateSingleton<ForwardedHeadersOptions>(configuration.GetSection(nameof(ApplicationOptions.ForwardedHeaders)))
				.Configure<ForwardedHeadersOptions>(options =>
					{
						options.KnownNetworks.Clear();
						options.KnownProxies.Clear();
					})
				.ConfigureAndValidateSingleton<GraphQlOptions>(configuration.GetSection(nameof(ApplicationOptions.GraphQL)))
				.ConfigureAndValidateSingleton<RequestExecutorOptions>(configuration.GetSection(nameof(ApplicationOptions.GraphQL))
						.GetSection(nameof(GraphQlOptions.Request)))
				.ConfigureAndValidateSingleton<KestrelServerOptions>(configuration.GetSection(nameof(ApplicationOptions.Kestrel)));

		/// <summary>
		/// Configures caching for the application. Registers the <see cref="IDistributedCache"/> and
		/// <see cref="IMemoryCache"/> types with the services collection or IoC container. The
		/// <see cref="IDistributedCache"/> is intended to be used in cloud hosted scenarios where there is a shared
		/// cache, which is shared between multiple instances of the application. Use the <see cref="IMemoryCache"/>
		/// otherwise.
		/// </summary>
		/// <param name="services">The services.</param>
		/// <returns>The services with caching services added.</returns>
		private static IServiceCollection AddCustomCaching(this IServiceCollection services) =>
			services.AddMemoryCache()
				// Adds IDistributedCache which is a distributed cache shared between multiple servers. This adds a
				// default implementation of IDistributedCache which is not distributed. You probably want to use the
				// Redis cache provider by calling AddDistributedRedisCache.
				.AddDistributedMemoryCache();

		/// <summary>
		/// Add cross-origin resource sharing (CORS) services and configures named CORS policies (See
		/// https://docs.asp.net/en/latest/security/cors.html).
		/// </summary>
		/// <param name="services">The services.</param>
		/// <returns>The services with caching services added.</returns>
		private static IServiceCollection AddCustomCors(this IServiceCollection services) =>
			services.AddCors(
				options =>
					// Create named CORS policies here which you can consume using application.UseCors("PolicyName")
					// or a [EnableCors("PolicyName")] attribute on your controller or action.
					options.AddPolicy(
						CorsPolicyNames.AllowAny,
						x => x
							.AllowAnyOrigin()
							.AllowAnyMethod()
							.AllowAnyHeader()));

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
				.AddAuthorization()
				.AddDirectiveType<DeferDirectiveType>()
				.AddTypes()
				.AddProjections()
				.AddFiltering()
				.AddSorting()
				.AddApolloTracing() // onDemand: add "GraphQl-tracing": 1 to http Header
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
					"text/plain", "text/html", "application/json", "application/xml", "text/css", "text/json",
					"font/woff2", "application/javascript", "image/x-icon", "image/png", "image/svg"
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