using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Types;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.UseCases.AppFolder.AppConfigParser;
using SmartHub.Domain;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.Init
{
	/// <summary>
	/// Endpoint for initialize queries.
	/// </summary>
	[ExtendObjectType(Name = "RootQueries")]
	[GraphQLDescription("All queries for the initialization service.")]
	public class InitQueries
	{
		// TODO move to separate query file damit das besser gesplitter ist?
		/// <summary>
		/// Gets the application information.
		/// </summary>
		/// <param name="appConfigService">The AppConfigService.</param>
		/// <returns>The AppConfig.</returns>
		[Authorize]
		public AppConfig GetAppConfig([Service] IAppConfigService appConfigService)
		{
			return appConfigService.GetConfig();
		}

		/// <summary>
		/// Gets an indication if users exist.
		/// </summary>
		/// <param name="userRepository">The user repository</param>
		/// <returns>Returns true if there are already users created.</returns>
		public async Task<bool> UsersExist([Service] IIdentityService identityService)
		{
			return await identityService.UsersExistAsync();
		}

		/// <summary>
		/// Gets an indication if the app is active (a smarthome is created).
		/// </summary>
		/// <param name="appConfigService">The AppConfigService.</param>
		/// <returns>Returns true if the app is initialized.</returns>
		public bool ApplicationIsActive([Service] IAppConfigService appConfigService)
		{
			return appConfigService.GetConfig().IsActive;
		}
	}
}