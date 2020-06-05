using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
				options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
				options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
				options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			})
				.SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

			// Http
			services.AddHttpClient();

			// HttpContext Accessor
			services.AddHttpContextAccessor();
		}
	}
}
