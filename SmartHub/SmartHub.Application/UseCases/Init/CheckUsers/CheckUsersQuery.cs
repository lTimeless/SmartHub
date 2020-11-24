using MediatR;
using SmartHub.Application.Common.Models;

namespace SmartHub.Application.UseCases.Init.CheckUsers
{
	public record CheckUsersQuery : IRequest<Response<bool>>
	{
	}
}