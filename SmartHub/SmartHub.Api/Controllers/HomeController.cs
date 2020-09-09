using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHub.Application.UseCases.Entity.Homes.Read;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SmartHub.Application.UseCases.Entity.Homes.Create;
using SmartHub.Application.UseCases.Entity.Homes.Patch;

namespace SmartHub.Api.Controllers
{
	public class HomeController : BaseController
	{
		/// <summary>
		/// Gets the home
		/// </summary>
		/// <returns>The Home</returns>
		[HttpGet]
		[AllowAnonymous]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Get()
		{
			return Ok(await Mediator.Send(new HomesReadQuery()));
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
		public async Task<IActionResult> Put([FromBody] HomePatchCommand value)
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
