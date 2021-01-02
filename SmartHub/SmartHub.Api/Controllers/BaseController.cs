using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace SmartHub.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public abstract class BaseController : ControllerBase
	{
	}
}
