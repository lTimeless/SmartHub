using MediatR;
using SmartHub.Application.Common.Models;
using SmartHub.Application.UseCases.Entity.Users;

namespace SmartHub.Application.UseCases.Identity.WhoAmI
{
    public class WhoAmIQuery : IRequest<Response<UserDto>>
    {
    }
}