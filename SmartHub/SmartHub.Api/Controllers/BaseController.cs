using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SmartHub.Domain.Common.Constants;

namespace SmartHub.Api.Controllers
{
	[Route(ApiRoutes.ApiControllerBase)]
	[ApiController]
	public class BaseController : ControllerBase
	{
		private IMediator _mediator;

		protected IMediator Mediator => _mediator ??=
			HttpContext.RequestServices.GetService<IMediator>();
	}
}
