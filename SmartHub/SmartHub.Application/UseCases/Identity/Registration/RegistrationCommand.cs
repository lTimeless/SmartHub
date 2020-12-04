
using MediatR;
using SmartHub.Application.Common.Models;

namespace SmartHub.Application.UseCases.Identity.Registration
{
	public record RegistrationCommand : IRequest<Response<AuthResponseDto>>
	{
		public string Username { get; init; }
		public string Password { get; init; }
		public string Role { get; init; }
	}
}
