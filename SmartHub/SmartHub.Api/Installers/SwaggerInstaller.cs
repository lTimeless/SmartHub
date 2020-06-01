using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace SmartHub.Api.Installers
{
	public class SwaggerInstaller : IInstaller
	{
		public void InstallServices(IServiceCollection services, IConfiguration configuration)
		{
			ConfigureSwagger(services);
		}

		private static void ConfigureSwagger(IServiceCollection services)
		{
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "v1",
					Title = "SmartHub API",
					Description = "A Smarthome ASP.NET Core Server",
					// TermsOfService = new Uri("https://example.com/terms"),
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

				// Set the comments path for the Swagger JSON and UI.
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				c.IncludeXmlComments(xmlPath);
			});
		}
	}
}
