using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatR.Pipeline;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Common.Enums;

namespace SmartHub.Application.Common.Behaviours
{
	public class CurrentUserBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserAccessor _userAccessor;
        private readonly CurrentUser _currentUser;

        public CurrentUserBehaviour(IUserAccessor currentUserService, CurrentUser currentUser, IUnitOfWork unitOfWork)
        {
            _userAccessor = currentUserService;
            _currentUser = currentUser;
            _unitOfWork = unitOfWork;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var userName = _userAccessor.GetCurrentUsername();
            var user = await _unitOfWork.UserRepository.GetUserByName(userName);
            var name = typeof(TRequest).Name;

            if (userName == Roles.System.ToString() && (name == "LoginQuery" || name == "RegisRegistrationCommand" || name == "CheckHomeQuery" || name == "CheckUsersQuery" ))
            {
                userName = "Anonymous";
            }
            _currentUser.User = user;
            _currentUser.RequesterName = userName;
        }
    }
}