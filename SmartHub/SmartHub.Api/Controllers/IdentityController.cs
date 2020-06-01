using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHub.Application.UseCases.Identity.Login;
using SmartHub.Application.UseCases.Identity.Registration;
using System.Threading.Tasks;

namespace SmartHub.Api.Controllers
{
	[AllowAnonymous]
	public class IdentityController : BaseController
	{
		// POST: api/Identity
		[HttpPost("login")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> Login([FromBody] LoginQuery value)
		{
			var result = await Mediator.Send(value);
			return Ok(result);
		}

		[HttpPost("registration")]
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
