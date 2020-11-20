using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SmartHub.Application.UseCases.Init.App;
using SmartHub.Application.UseCases.Init.CheckHome;
using SmartHub.Application.UseCases.Init.CheckUsers;
using SmartHub.Domain;

namespace SmartHub.Api.Controllers
{
	public class InitController : BaseController
    {

		private readonly IOptions<AppConfig> _appConfig;

		public InitController(IOptions<AppConfig> appConfig)
		{
			_appConfig = appConfig;
		}

		/// <summary>
		/// Gets the application information.
		/// </summary>
		/// <returns>The Home</returns>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[AllowAnonymous]
		public IActionResult Get()
		{
			var response = Application.Common.Models.Response.Ok(_appConfig.Value);
			return Ok(response);
		}

		/// <summary>
		/// Initializes updates the application.
		/// </summary>
		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Post([FromBody] AppConfigInitCommand value)
		{
			return Ok(await Mediator.Send(value).ConfigureAwait(false));
		}


		/// <summary>
		/// Gets an indication if the app is active (a smarthome is created).
		/// </summary>
		/// <returns>bool true= exist</returns>
		[HttpGet("checkHome")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetHome()
        {
            return Ok(await Mediator.Send(new CheckHomeQuery()).ConfigureAwait(false));
        }

        /// <summary>
        /// Gets an indication if users exist.
        /// </summary>
        /// <returns>bool true= exist</returns>
        [HttpGet("checkUsers")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await Mediator.Send(new CheckUsersQuery()).ConfigureAwait(false));
        }
    }
}