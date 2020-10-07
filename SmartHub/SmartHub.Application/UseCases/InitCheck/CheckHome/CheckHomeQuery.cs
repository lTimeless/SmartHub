using MediatR;
using SmartHub.Application.Common.Models;

namespace SmartHub.Application.UseCases.InitCheck.CheckHome
{
    public class CheckHomeQuery : IRequest<Response<bool>>
    {

    }
}