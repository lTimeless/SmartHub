using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Domain.Common.Constants;
using SmartHub.Domain.Common.Options;
using SmartHub.Domain.Entities;
using SmartHub.Infrastructure.Database;
using SmartHub.Infrastructure.Database.Repositories;
using SmartHub.Infrastructure.Services.FileSystem;
using SmartHub.Infrastructure.Services.Http;
using SmartHub.Infrastructure.Services.Identity;
using SmartHub.Infrastructure.Services.Initialization;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHub.Infrastructure
{
	/// <summary>
	///     Adds Auth, Services, Repos, etc from the persistence context into the DI container.
	/// </summary>
	public static class ServiceExtension
	{
		public static IServiceCollection AddInfrastructurePersistence(this IServiceCollection services,
			IConfiguration configuration)
		{
			return services.AddDbContext(configuration)
				.AddCustomCors()
				.AddCustomAuthorization(configuration)
				.AddRepositories()
				.AddServices()
				.AddBackgroundServices();
		}

		private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = CreateConnectionString(configuration);
			services.AddDbContext<AppDbContext>(builder =>
			{
				builder.UseLazyLoadingProxies();
				// Change to LogLevel.Information or lower wo see the generated sql statements
				builder.LogTo(Console.WriteLine, LogLevel.Error);
				builder.UseNpgsql(connectionString,
					options =>
					{
						options.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
						options.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
					});
			});
			// services.AddScoped<IAppDbContext>(provider => provider.GetService<AppDbContext>()!);

			services.AddIdentity<User, Role>(options =>
				{
					options.SignIn.RequireConfirmedAccount = true;
					options.User.RequireUniqueEmail = false;
				})
				.AddEntityFrameworkStores<AppDbContext>()
				.AddDefaultTokenProviders();

			return services;
		}

		private static IServiceCollection AddCustomCors(this IServiceCollection services)
		{
			return services.AddCors(options =>
			{
				options.AddPolicy(AuthConstants.CorsPolicies.AllowAny,
					builder => builder
						.WithOrigins("http://localhost:8080", "http://localhost:4200")
						.AllowAnyMethod()
						.AllowAnyHeader());
			});
		}

		private static IServiceCollection AddCustomAuthorization(this IServiceCollection services,
			IConfiguration configuration)
		{
			var jwtOptions = configuration.GetSection(nameof(ApplicationOptions.Jwt)).Get<JwtOptions>();

			var tokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				// TokenUtils.ValidateAndGenerateToken(
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.Key)),
				ValidateIssuer = true,
				ValidateAudience = true,
				RequireExpirationTime = false,
				ValidateLifetime = true,
				// ClockSkew = TimeSpan.Zero,
				ValidIssuer = jwtOptions.Issuer,
				ValidAudience = jwtOptions.Audience
			};
			services.AddSingleton(tokenValidationParameters);

			services.AddAuthentication(x =>
				{
					x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
					x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
					x.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
				}).AddJwtBearer(options =>
				{
					options.RequireHttpsMetadata = false;
					options.SaveToken = true;
					options.TokenValidationParameters = tokenValidationParameters;
					options.SaveToken = true;
					options.Events = new()
					{
						OnMessageReceived = context =>
						{
							if (context.Request.Cookies.ContainsKey("SmartHub-Access-Token"))
							{
								context.Token = context.Request.Cookies["SmartHub-Access-Token"];
							}

							return Task.CompletedTask;
						}
					};
				})
				// 	.AddCookie(options =>
				// {
				// 	options.Cookie.HttpOnly = true;
				// 	options.ExpireTimeSpan = TimeSpan.FromHours(1);
				// 	options.SlidingExpiration = true;
				// 	options.Cookie.SameSite = SameSiteMode.Strict;
				// 	options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
				// 	options.Cookie.IsEssential = true;
				// 	options.LoginPath = "/login";
				// 	// options.LogoutPath = "/logout";
				// })
				;

			return services.AddAuthorization(options => options
				.AddPolicy(AuthConstants.AuthorizationPolicies.Admin, x => x.RequireAuthenticatedUser()));
		}

		private static IServiceCollection AddRepositories(this IServiceCollection services)
		{
			services.AddScoped(typeof(IBaseRepositoryAsync<>), typeof(BaseRepositoryAsync<>));
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IDbSeeder, DbSeeder>();
			return services;
		}

		private static IServiceCollection AddBackgroundServices(this IServiceCollection services)
		{
			services.AddHostedService<InitializationService>();
			return services;
		}

		private static IServiceCollection AddServices(this IServiceCollection services)
		{
			// Identity
			services.AddScoped<IIdentityService, IdentityService>();
			// Directory
			services.AddTransient<IFileService, FileService>();
			services.AddTransient<IDirectoryService, DirectoryService>();
			// Http
			services.AddScoped<IPingService, PingService>();
			services.AddScoped<IHttpService, HttpService>();
			return services;
		}

		#region Helper

		private static string CreateConnectionString(IConfiguration configuration)
		{
			var oldConnectionString = configuration["Persistence:ConnectionStrings:sqlConnection"];
			var argsUser = configuration.GetValue<string>("Pgsql_User");
			var argsPwd = configuration.GetValue<string>("Pgsql_pwd");
			var dictionary = oldConnectionString
				.Remove(oldConnectionString.Length - 1)
				.Split(";")
				.Select(x => x.Split("="))
				.ToDictionary(x => x[0], x => x[1]);
			if (dictionary.ContainsKey("User ID"))
			{
				// TODO: hier hat die appsetting immer höchste prio auch wenn argsUser befüllt ist !!!
				dictionary["User ID"] = dictionary["User ID"].Contains("<") ? argsUser : dictionary["User ID"];
			}

			if (dictionary.ContainsKey("Password"))
			{
				dictionary["Password"] = dictionary["Password"].Contains("<") ? argsPwd : dictionary["Password"];
			}

			return string.Concat(
				string.Join(";", dictionary.Select(x => x.Key + "=" + x.Value)),
				";");
		}

		#endregion
	}
}