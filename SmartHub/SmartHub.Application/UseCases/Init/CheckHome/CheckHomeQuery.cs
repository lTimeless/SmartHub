using MediatR;
using SmartHub.Application.Common.Models;

namespace SmartHub.Application.UseCases.Init.CheckHome
{
	public record CheckHomeQuery : IRequest<Response<bool>>
	{
	}
}