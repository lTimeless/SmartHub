using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Common.Enums;

namespace SmartHub.Application.Common.Behaviours
{
	public class CurrentUserBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
    {
        private readonly IUserAccessor _userAccessor;
        private readonly CurrentUser _currentUser;
		private readonly IUserRepository _userRepository;

		public CurrentUserBehaviour(IUserAccessor currentUserService, CurrentUser currentUser, IUserRepository userRepository)
		{
			_userAccessor = currentUserService;
			_currentUser = currentUser;
			_userRepository = userRepository;
		}

		public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var userName = _userAccessor.GetCurrentUsername();
            var user = await _userRepository.GetUserByName(userName);
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