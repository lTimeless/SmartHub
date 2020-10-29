using MediatR;
using SmartHub.Application.Common.Models;

namespace SmartHub.Application.UseCases.InitCheck.CheckUsers
{
    public class CheckUsersQuery: IRequest<Response<bool>>
    {

    }
}