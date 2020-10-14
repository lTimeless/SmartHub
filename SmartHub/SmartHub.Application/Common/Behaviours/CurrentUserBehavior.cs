using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;

namespace SmartHub.Application.Common.Behaviours
{
	public class CurrentUserBehavior<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserAccessor _userAccessor;
        private readonly CurrentUser _currentUser;

        public CurrentUserBehavior(IUserAccessor currentUserService, CurrentUser currentUser, IUnitOfWork unitOfWork)
        {
            _userAccessor = currentUserService;
            _currentUser = currentUser;
            _unitOfWork = unitOfWork;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var userName = _userAccessor.GetCurrentUsername();
            var user = await _unitOfWork.UserRepository.GetUserByName(userName);
            _currentUser.User = user;
            _currentUser.RequesterName = userName;
        }
    }
}