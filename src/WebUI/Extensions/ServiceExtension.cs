using Boxed.AspNetCore;
using HotChocolate.Execution.Options;
using HotChocolate.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Polly;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.UseCases.Entity.Devices.ExtendObjectType;
using SmartHub.Application.UseCases.Entity.Users;
using SmartHub.Domain.Common.Options;
using SmartHub.WebUI.GraphQl;
using SmartHub.WebUI.Services;
using System;
using System.IO.Compression;
using System.Linq;

namespace SmartHub.WebUI.Extensions
{
	public static class ServiceExtension
	{
		public static IServiceCollection AddApiLayer(this IServiceCollection services, IHostEnvironment environment,
			IConfiguration configuration)
		{
			services.AddCustomCaching()
				.AddCustomOptions(configuration)
				.AddCustomRouting()
				.AddCustomResponseCompression(configuration)
				.AddCustomHealthChecks()
				.AddHttpContextAccessor()
				.AddHttpClientFactory();

			services.AddServerTiming()
				.AddControllers()
				.AddSpaStaticFiles()
				// .AddSignalR()
				.AddCustomGraphQl(configuration)
				.AddScoped<ICurrentUserService, CurrentUserService>();

			return services;
		}

		/// <summary>
		///     Configures caching for the application. Registers the <see cref="IDistributedCache" /> and
		///     <see cref="IMemoryCache" /> types with the services collection or IoC container. The
		///     <see cref="IDistributedCache" /> is intended to be used in cloud hosted scenarios where there is a shared
		///     cache, which is shared between multiple instances of the application. Use the <see cref="IMemoryCache" />
		///     otherwise.
		/// </summary>
		/// <param name="services">The services.</param>
		/// <returns>The services with caching services added.</returns>
		private static IServiceCollection AddCustomCaching(this IServiceCollection services)
		{
			return services.AddMemoryCache()
				// Adds IDistributedCache which is a distributed cache shared between multiple servers. This adds a
				// default implementation of IDistributedCache which is not distributed. You probably want to use the
				// Redis cache provider by calling AddDistributedRedisCache.
				.AddDistributedMemoryCache();
		}


		/// <summary>
		///     Configures the settings by binding the contents of the appsettings.json file to the specified Plain Old CLR
		///     Objects (POCO) and adding <see cref="IOptions{TOptions}" /> objects to the services collection.
		/// </summary>
		/// <param name="services">The services.</param>
		/// <param name="configuration">The configuration.</param>
		/// <returns>The services with caching services added.</returns>
		private static IServiceCollection AddCustomOptions(this IServiceCollection services,
			IConfiguration configuration)
		{
			return services
				.ConfigureAndValidateSingleton<ApplicationOptions>(configuration)
				.ConfigureAndValidateSingleton<JwtOptions>(configuration.GetSection(nameof(ApplicationOptions.Jwt)))
				.ConfigureAndValidateSingleton<CacheProfileOptions>(
					configuration.GetSection(nameof(ApplicationOptions.CacheProfiles)))
				.ConfigureAndValidateSingleton<CompressionOptions>(
					configuration.GetSection(nameof(ApplicationOptions.Compression)))
				// .ConfigureAndValidateSingleton<ForwardedHeadersOptions>(
				// 	configuration.GetSection(nameof(ApplicationOptions.ForwardedHeaders)))
				// .Configure<ForwardedHeadersOptions>(options =>
				// {
				// 	options.KnownNetworks.Clear();
				// 	options.KnownProxies.Clear();
				// })
				.ConfigureAndValidateSingleton<GraphQlOptions>(
					configuration.GetSection(nameof(ApplicationOptions.GraphQl)))
				.ConfigureAndValidateSingleton<RequestExecutorOptions>(configuration
					.GetSection(nameof(ApplicationOptions.GraphQl))
					.GetSection(nameof(GraphQlOptions.Request)));
		}


		/// <summary>
		///     Add custom routing settings which determines how URL's are generated.
		/// </summary>
		/// <param name="services">The services.</param>
		/// <returns>The services with caching services added.</returns>
		private static IServiceCollection AddCustomRouting(this IServiceCollection services)
		{
			return services.AddRouting(options => options.LowercaseUrls = true);
		}


		/// <summary>
		///     Adds dynamic response compression to enable GZIP compression of responses. This is turned off for HTTPS
		///     requests by default to avoid the BREACH security vulnerability.
		/// </summary>
		/// <param name="services">The services.</param>
		/// <param name="configuration">The configuration.</param>
		/// <returns>The services with caching services added.</returns>
		private static IServiceCollection AddCustomResponseCompression(this IServiceCollection services,
			IConfiguration configuration)
		{
			return services
				.Configure<BrotliCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal)
				.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal)
				.AddResponseCompression(
					options =>
					{
						options.EnableForHttps = true;
						var customMimeTypes = configuration
							.GetSection(nameof(ApplicationOptions.Compression))
							.Get<CompressionOptions>()?.MimeTypes ?? Enumerable.Empty<string>();
						options.MimeTypes = customMimeTypes.Concat(ResponseCompressionDefaults.MimeTypes);

						options.Providers.Add<BrotliCompressionProvider>();
						options.Providers.Add<GzipCompressionProvider>();
					});
		}

		private static IServiceCollection AddCustomHealthChecks(this IServiceCollection services)
		{
			return services
				.AddHealthChecks()
				// Add health checks for external dependencies here. See https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks
				.Services;
			//    var connectionString = config.GetConnectionString("sqlConnection");
			//    services.AddHealthChecks()
			//        .AddDbContextCheck<AppDbContext>()
			//        .AddNpgSql(connectionString)
			//        .AddCheck<ServicesHealthCheck>("All Services")
			//        ;

			//    services.AddSingleton<ServicesHealthCheck>();
		}

		private static IServiceCollection AddSpaStaticFiles(this IServiceCollection services)
		{
			// In production, the files will be served from this directory
			services.AddSpaStaticFiles(options =>
			{
				options.RootPath = "wwwroot";
			});
			return services;
		}

		private static IServiceCollection AddCustomGraphQl(this IServiceCollection services,
			IConfiguration configuration)
		{
			var graphQlOptions = configuration.GetSection(nameof(ApplicationOptions.GraphQl)).Get<GraphQlOptions>();
			return services.AddGraphQLServer()
					.AddQueryType<RootQueryType>()
					.AddMutationType<RootMutationType>()
					.AddAuthorization()
					.AddDirectiveType<DeferDirectiveType>()
					.AddType<ExtendDeviceType>()
					// .AddProjectScalarTypes()
					// .AddProjectDirectives()
					// .AddProjectDataLoaders()
					// .AddProjectTypes()
					.AddType<UserResolver>()
					.AddProjections()
					.AddFiltering()
					.AddSorting()
					.AddApolloTracing() // onDemand: add "GraphQl-tracing": 1 to http Header
					// .EnableRelaySupport()
					// .TrimTypes()
					// .ModifyOptions(options => options.UseXmlDocumentation = false)
					.AddMaxComplexityRule(graphQlOptions.MaxAllowedComplexity)
					.AddMaxExecutionDepthRule(graphQlOptions.MaxAllowedExecutionDepth)
					// .SetPagingOptions(graphQlOptions.Paging)
					.SetRequestOptions(() => graphQlOptions.Request)
					.Services
				;
		}

		private static IServiceCollection AddControllers(this IServiceCollection service)
		{
			service.AddControllers(opt =>
				{
					// var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
					// opt.Filters.Add(new AuthorizeFilter(policy));
				})
				.SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
			return service;
		}

		private static void AddHttpClientFactory(this IServiceCollection services)
		{
			services.AddHttpClient("SmartDevices", x =>
				{
					x.DefaultRequestHeaders.Add("User-Agent", "smartHub");
				})
				.AddTransientHttpErrorPolicy(x => x.WaitAndRetryAsync(3,
					retryAttempt => TimeSpan.FromMilliseconds(retryAttempt * 100)));
		}
	}
}