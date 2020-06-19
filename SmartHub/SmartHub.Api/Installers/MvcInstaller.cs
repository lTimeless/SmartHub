using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NodaTime;
using NodaTime.Serialization.JsonNet;

namespace SmartHub.Api.Installers
{
	public class MvcInstaller : IInstaller
	{
		public void InstallServices(IServiceCollection services, IConfiguration configuration)
		{
			services.AddControllers(opt =>
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



			// HttpContext Accessor
			services.AddHttpContextAccessor();
		}
	}
}
