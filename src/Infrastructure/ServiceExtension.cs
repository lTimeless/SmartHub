using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SmartHub.Application.Common.Helpers;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Domain.Common.Settings;
using SmartHub.Domain.Entities;
using SmartHub.Infrastructure.Database;
using SmartHub.Infrastructure.Database.Repositories;
using SmartHub.Infrastructure.Services.FileSystem;
using SmartHub.Infrastructure.Services.Http;
using SmartHub.Infrastructure.Services.Identity;
using SmartHub.Infrastructure.Services.Initialization;
using System.Threading.Tasks;

namespace SmartHub.Infrastructure
{
	public static class ServiceExtension
    {
        public static IServiceCollection AddInfrastructurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            // Db contexts
            services.AddDbContext(configuration);
            // Authentication and Authorization
            services.AddAuth(configuration);
            // Repositories
            services.AddRepositories();
            // Services
            services.AddServices();
            services.AddBackgroundServices();
            return services;
        }

        private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
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
            services.AddScoped<IAppDbContext>(provider => provider.GetService<AppDbContext>());

            services.AddIdentity<User, Role>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.User.RequireUniqueEmail = false;
                })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
        }

        private static void AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .WithOrigins("http://localhost:8080", "http://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

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
	            options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(TokenUtils.ValidateAndGenerateToken(configuration["JwtSettings:Key"]))),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    //RequireExpirationTime = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"]
                };

                options.SaveToken = true;
                options.Events = new();
                options.Events.OnMessageReceived = context => {
	                if (context.Request.Cookies.ContainsKey("X-Access-Token"))
	                {
		                context.Token = context.Request.Cookies["X-Access-Token"];
	                }
	                return Task.CompletedTask;
                };
            }).AddCookie(options =>
            {
	            options.Cookie.SameSite = SameSiteMode.Strict;
	            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
	            options.Cookie.IsEssential = true;
            });

            services.AddAuthorization(options =>
            {
                // TODO: will be reImplemented at a later date
                // options.AddPolicy("AdminPolicy" , policy => policy.AddRequirements(new UserAuthRequirement()));
            });
        }

        private static void AddRepositories(this IServiceCollection services)
        {
			services.AddScoped(typeof(IBaseRepositoryAsync<>), typeof(BaseRepositoryAsync<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDbSeeder, DbSeeder>();
        }

        private static void AddBackgroundServices(this IServiceCollection services)
        {
            services.AddHostedService<InitializationService>();
        }

        private static void AddServices(this IServiceCollection services)
        {
	        // Identity
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<TokenGenerator>();
            // Directory
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IDirectoryService, DirectoryService>();
            // Http
            services.AddScoped<IPingService, PingService>();
            services.AddScoped<IHttpService, HttpService>();
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
                string.Join(";",dictionary.Select(x => x.Key + "=" + x.Value)),
                ";" );
        }
        #endregion
    }
}