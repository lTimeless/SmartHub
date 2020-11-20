using MediatR;
using SmartHub.Application.Common.Models;

namespace SmartHub.Application.UseCases.Init.CheckUsers
{
	public class CheckUsersQuery : IRequest<Response<bool>>
	{

	}
}