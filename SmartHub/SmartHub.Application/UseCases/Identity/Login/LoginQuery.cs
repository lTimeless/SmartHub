using MediatR;
using SmartHub.Application.Common.Models;

namespace SmartHub.Application.UseCases.Identity.Login
{
	public record LoginQuery : IRequest<Response<AuthResponseDto>>
	{
		public string UserName { get; init; }
		public string Password { get; init; }
	}
}
