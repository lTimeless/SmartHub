using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace SmartHub.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BaseController : ControllerBase
	{
		private IMediator _mediator;

		protected IMediator Mediator => _mediator ??=
			HttpContext.RequestServices.GetService<IMediator>();
	}
}
