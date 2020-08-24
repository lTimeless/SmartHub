using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHub.Application.UseCases.Identity.Login;
using SmartHub.Application.UseCases.Identity.Registration;
using System.Threading.Tasks;
using SmartHub.Application.UseCases.Identity;
using SmartHub.Domain.Common.Constants;

namespace SmartHub.Api.Controllers
{
	[AllowAnonymous]
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
		[HttpPost(ApiRoutes.IdentityRoutes.Login)]
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
		[HttpPost(ApiRoutes.IdentityRoutes.Registration)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> Registration([FromBody] RegistrationCommand value)
		{
			var result = await Mediator.Send(value);
			return Ok(result);
		}
	}
}
