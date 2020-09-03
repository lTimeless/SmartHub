using System;
using System.Linq;
using System.Text;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Interfaces.Repositories;
using SmartHub.Domain.Common.Settings;
using SmartHub.Domain.Entities;
using SmartHub.Infrastructure.Database;
using SmartHub.Infrastructure.Database.Repositories;
using SmartHub.Infrastructure.Services.Auth;

namespace SmartHub.Infrastructure
{
    public static class ServiceExtension
    {
        public static void AddInfrastrucurePersistance(this IServiceCollection services, IConfiguration configuration)
        {
            // Db contexts
            services.AddDbContext(configuration);
            services.AddHangfireConfiguration(configuration);
            // Authentication and Authorization
            services.AddAuth(configuration);
            // Repositories
            services.AddRepositories();
            // Services
            services.AddServices();
        }

        private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("Use_InMemoryDb"))
            {
                services.AddDbContext<AppDbContext>(options =>
                    options.UseInMemoryDatabase("SmartHubDb"));
            }
            else
            {
                var connectionString = CreateConnectionString(configuration);

                services.AddDbContext<AppDbContext>(builder =>
                    {
                        builder.EnableSensitiveDataLogging(false);
                        builder.UseLazyLoadingProxies();
                        builder.UseNpgsql(connectionString,
                            options =>
                            {
                                options.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                                options.UseNodaTime();
                            });
                    })
                    .BuildServiceProvider();
            }
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
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JwtSettings:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    //RequireExpirationTime = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"]
                };
            });

            services.AddAuthorization(options =>
            {
                // TODO: wird neu gemacht
                // options.AddPolicy("AdminPolicy" , policy => policy.AddRequirements(new UserAuthRequirement()));
                // options.AddPolicy("AllAuthPolicy" , policy => policy.RequireRole("role" , CustomRoles.All));
                //options.AddPolicy("TestPolicy" , policy => policy.AddRequirements(new Requirement()));
            });

            // Handler for Authorization attribute and the "adminpolicy" on asp.net core routes
            // services.AddSingleton<IAuthorizationHandler , UserAuthHandler>();
        }

        private static void AddHangfireConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = CreateConnectionString(configuration);
            services.AddHangfire(x => x.UsePostgreSqlStorage(connectionString));
            services.AddHangfireServer();
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IHomeRepository, HomeRepositoryAsync>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<SeedDatabase>();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IUserAccessor, UserAccessor>();
        }

        #region Helper
        private static string CreateConnectionString(IConfiguration configuration)
        {
            var oldConnectionString = configuration["ConnectionStrings:sqlConnection"];
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