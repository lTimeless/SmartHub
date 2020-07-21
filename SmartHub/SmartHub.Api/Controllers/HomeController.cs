using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHub.Application.UseCases.Entity.Homes.Create;
using SmartHub.Application.UseCases.Entity.Homes.Read;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SmartHub.Application.UseCases.Entity.Homes.Update;

namespace SmartHub.Api.Controllers
{
	public class HomeController : BaseController
	{
		/// <summary>
		/// GET: api/Home
		/// </summary>
		/// <returns>List of Homes</returns>
		[AllowAnonymous] // TODO: Create another endpoint which will just return less infos about the home
		// only name
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> Get()
		{
			return Ok(await Mediator.Send(new HomesReadQuery()));
		}

		/// <summary>
		/// Creates a new Home
		/// </summary>
		/// /// <remarks>
		/// Sample request:
		///
		///     POST /home
		///     {
		///        "name": "Test",
		///        "description": "Test_2",
		///     }
		///
		/// </remarks>
		/// <param name="value"></param>
		/// <returns>A newly created Home and default Setting Object</returns>
		/// <response code="201">Returns the newly created item</response>
		/// <response code="400">If the item is null</response>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> Post([FromBody] HomeCreateCommand value)
		{
			return Ok(await Mediator.Send(value).ConfigureAwait(false));
		}

		// PUT: api/Home
		[HttpPut]
		public async Task<IActionResult> Put([FromBody] HomeUpdateCommand value)
		{
			return Ok(await Mediator.Send(value).ConfigureAwait(false));
		}

		// DELETE: api/ApiWithActions/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
			throw new NotSupportedException($"[{nameof(Delete)}]Not supported");
		}
	}
}
