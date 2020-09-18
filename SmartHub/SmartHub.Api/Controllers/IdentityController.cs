using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHub.Application.UseCases.Identity.Login;
using SmartHub.Application.UseCases.Identity.Registration;
using System.Threading.Tasks;
using SmartHub.Application.UseCases.Identity;
using SmartHub.Application.UseCases.Identity.Me.Read;
using SmartHub.Application.UseCases.Identity.Me.Update;

namespace SmartHub.Api.Controllers
{

	public class IdentityController : BaseController
	{
		// POST: api/Identity
		/// <summary>
		/// Login path
		/// </summary>
		/// <param name="value">Login parameters</param>
		/// <returns><see cref="AuthResponseDto"/>>AuthResponse with jwt</returns>
		/// <response code="200">Returns if everything went ok</response>
		/// <response code="401">If you are not authorized</response>
		[AllowAnonymous]
		[HttpPost("login")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> Login([FromBody] LoginQuery value)
		{
			var result = await Mediator.Send(value);
			return Ok(result);
		}

		// POST: api/Identity
		/// <summary>
		/// Registration path
		/// </summary>
		/// <param name="value">Registration parameters</param>
		/// <returns><see cref="AuthResponseDto"/>>AuthResponse with jwt</returns>
		/// <response code="200">Returns if everything went ok</response>
		/// <response code="401">If you are not authorized</response>
		[AllowAnonymous]
		[HttpPost("registration")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> Registration([FromBody] RegistrationCommand value)
		{
			var result = await Mediator.Send(value);
			return Ok(result);
		}

		/// <summary>
		/// Retrieves user
		/// </summary>
		/// <returns>The user who requests this.</returns>
		/// <response code="200">Returns myself userDto</response>
		/// <response code="401">Returns unauthorized</response>
		[HttpGet("me")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> GetMyself(CancellationToken cancellationToken)
		{
			return Ok(await Mediator.Send(new MeReadQuery(), cancellationToken));
		}

		/// <summary>
		/// Updates the own user data.
		/// </summary>
		[HttpPut("me")]
		public async Task<IActionResult> Put([FromBody] MeUpdateCommand value)
		{
			return Ok(await Mediator.Send(value));
		}
	}
}
