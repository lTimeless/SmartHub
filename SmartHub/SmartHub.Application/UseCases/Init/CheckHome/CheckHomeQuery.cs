using MediatR;
using SmartHub.Application.Common.Models;

namespace SmartHub.Application.UseCases.Init.CheckHome
{
	public class CheckHomeQuery : IRequest<Response<bool>>
	{

	}
}