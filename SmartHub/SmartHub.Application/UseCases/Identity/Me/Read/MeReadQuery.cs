using MediatR;
using SmartHub.Application.Common.Models;
using SmartHub.Application.UseCases.Entity.Users;

namespace SmartHub.Application.UseCases.Identity.Me.Read
{
    public record MeReadQuery : IRequest<Response<UserDto>>
    {
    }
}