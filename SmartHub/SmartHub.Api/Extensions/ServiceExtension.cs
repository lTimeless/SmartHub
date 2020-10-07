using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
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
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NodaTime;
using NodaTime.Serialization.JsonNet;
using Polly;
using SmartHub.Api.Validators;
using SmartHub.Domain.Common.Settings;

namespace SmartHub.Api.Extensions
{
	public static class ServiceExtension
	{
		public static IServiceCollection AddApiLayer(this IServiceCollection services, IConfiguration configuration, IHostEnvironment appEnvironment)
		{
			// Server configuration
			services.AddServerConfiguration(configuration, appEnvironment);
			// Swagger
			services.AddSwagger();
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

		private static void AddServerConfiguration(this IServiceCollection services, IConfiguration configuration, IHostEnvironment appEnvironment)
		{
			services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true);

			services.Configure<HostOptions>(configuration.GetSection("HostOptions"));

			// -------------- SmartHubSettings ---------------
			services.Configure<ApplicationSettings>(configuration.GetSection("SmartHub"));
			services.TryAddSingleton<IValidateOptions<ApplicationSettings>, ApplicationSettingsValidation>();
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

		private static void AddSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(c =>
			{
				// Set the comments path for the Swagger JSON and UI.
				c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "v1",
					Title = "SmartHub Api",
					Description = "This Api will be responsible for all data distribution and authorization.",
					Contact = new OpenApiContact
					{
						Name = "Maximilian Stümpfl",
						Email = string.Empty,
						Url = new Uri("https://github.com/lTimeless"),
					},
					License = new OpenApiLicense
					{
						Name = "Use under MIT",
						Url = new Uri("https://example.com/license"),
					}
				});

				var securityScheme = new OpenApiSecurityScheme
				{
					In = ParameterLocation.Header,
					Description = "Please insert JWT",
					Name = "Authorization",
					Type = SecuritySchemeType.Http,
					Scheme = "bearer",
					BearerFormat = "JWT",
					Reference = new OpenApiReference
					{
						Type = ReferenceType.SecurityScheme,
						Id = "Bearer"
					}
				};

				c.AddSecurityDefinition("Bearer", securityScheme);

				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{ securityScheme, new[] { "Bearer" } }
				});

			});
			services.AddSwaggerGenNewtonsoftSupport();
		}

		private static void AddControllers(this IServiceCollection service)
		{
			service.AddControllers(opt =>
				{
					var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
					opt.Filters.Add(new AuthorizeFilter(policy));
				}).AddNewtonsoftJson(options =>
				{
					var settings = options.SerializerSettings;

					settings.DateParseHandling = DateParseHandling.None;
					settings.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
					settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
					settings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
					settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
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
