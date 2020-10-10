using MediatR;
using SmartHub.Application.Common.Models;

namespace SmartHub.Application.UseCases.Entity.Homes.Read
{
	public class HomeGetQuery : IRequest<Response<HomeDto>>
	{
	}
}
