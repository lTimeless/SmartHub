using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;

namespace SmartHub.Application.UseCases.InitCheck.CheckUsers
{
    public class CheckUsersHandler : IRequestHandler<CheckUsersQuery ,Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CheckUsersHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<bool>> Handle(CheckUsersQuery request, CancellationToken cancellationToken)
        {
            var usersExist = await _unitOfWork.UserRepository.UsersExist().ConfigureAwait(false);
            return usersExist ? Response.Ok(true) : Response.Fail("No users exist.",false);
        }
    }
}