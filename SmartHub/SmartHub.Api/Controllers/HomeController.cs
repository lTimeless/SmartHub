using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHub.Application.UseCases.Entity.Homes.Create;
using SmartHub.Application.UseCases.Entity.Homes.Read;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SmartHub.Application.UseCases.Entity.Homes.Update;
using SmartHub.Domain.Common.Constants;

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
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> Get()
		{
			return Ok(await Mediator.Send(new HomesReadQuery()));
		}

		/// <summary>
		/// Creates the new Home
		/// </summary>
		/// <param name="value"></param>
		/// <returns>A newly created Home and default Setting Object</returns>
		/// <response code="201">Returns the newly created item</response>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> Post([FromBody] HomeCreateCommand value)
		{
			return Ok(await Mediator.Send(value).ConfigureAwait(false));
		}

		/// <summary>
		/// Partial updates the Home
		/// </summary>
		[HttpPatch]
		public async Task<IActionResult> Patch([FromBody] HomeUpdateCommand value)
		{
			return Ok();
		}


		/// <summary>
		/// Full updates the Home
		/// </summary>
		[HttpPut]
		public async Task<IActionResult> Put([FromBody] HomeUpdateCommand value)
		{
			return Ok(await Mediator.Send(value).ConfigureAwait(false));
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
