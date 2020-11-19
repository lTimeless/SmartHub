using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using SmartHub.Application.UseCases.Entity.Homes.Create;
using SmartHub.Application.UseCases.Entity.Homes.PartialUpdate;
using SmartHub.Application.UseCases.Entity.Homes.Read;
using SmartHub.Domain;

namespace SmartHub.Api.Controllers
{
	public class HomeController : BaseController
	{
		private readonly IOptions<Domain.AppConfig> _applicationConfig;

		public HomeController(IOptions<Domain.AppConfig> applicationConfig)
		{
			_applicationConfig = applicationConfig;
		}


		/// <summary>
		/// Gets the home
		/// </summary>
		/// <returns>The Home</returns>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Get()
		{
			return Ok(await Mediator.Send(new HomeGetQuery()).ConfigureAwait(false));
		}

		/// <summary>
		/// Partial updates the Home
		/// </summary>
		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Post([FromBody] HomeCreateCommand value)
		{
			return Ok(await Mediator.Send(value).ConfigureAwait(false));
		}

		[HttpGet("test")]
		[AllowAnonymous]
		public IActionResult GetAppConfig()
		{
			return Ok(_applicationConfig);
		}

		/// <summary>
		/// Partial updates the Home
		/// </summary>
		[HttpPatch]
		public async Task<IActionResult> Patch([FromBody] HomePatchCommand value)
		{
			return Ok(await Mediator.Send(value).ConfigureAwait(false));
		}


        /// <summary>
        /// Full updates the Home
        /// </summary>
        [HttpPut]
        public IActionResult Put([FromBody] HomePatchCommand value)
        {
            return Ok("This is not implemented at the moment.");
        }

        /// <summary>
        /// Deletes the Home
        /// </summary>
        [HttpDelete]
		public void Delete()
		{
			throw new NotSupportedException($"[{nameof(Delete)}]Not supported");
		}
	}
}
