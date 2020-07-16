using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SmartHub.Domain.Common.Settings;
using System;
using System.Text;
using SmartHub.Domain.Entities.Users;
using SmartHub.Infrastructure.Database;

namespace SmartHub.Api.Installers
{
	public class SecurityInstaller : IInstaller
	{
		public void InstallServices(IServiceCollection services, IConfiguration configuration)
		{
			ConfigureCors(services);
			ConfigureAuth(services, configuration);
		}

		private static void ConfigureAuth(IServiceCollection services, IConfiguration configuration)
		{
			var jwtSettings = new JwtSettings(configuration["JWTSettings:Secret"], TimeSpan.Parse(configuration["JWTSettings:TokenLifeTime"]));
			services.AddSingleton(jwtSettings);

			services.AddIdentityServer()
				.AddApiAuthorization<User, AppDbContext>();

			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(x =>
			{
				x.SaveToken = true;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
					ValidateIssuer = false,
					ValidateAudience = false,
					//RequireExpirationTime = false,
					//ValidateLifetime = true,
				};
			}).AddIdentityServerJwt();

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

		private static void ConfigureCors(IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy",
					builder => builder
						.WithOrigins("http://localhost:8080", "http://localhost:4200")
						.AllowAnyMethod()
						.AllowAnyHeader());
			});
		}
	}
}
