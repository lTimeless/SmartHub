using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.Common.Behaviours
{
    public class CurrentUserBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserAccessor _userAccessor;
        private readonly CurrentUser _currentUser;

        public CurrentUserBehaviour(IUserAccessor currentUserService, UserManager<User> userManager, CurrentUser currentUser)
        {
            _userAccessor = currentUserService;
            _userManager = userManager;
            _currentUser = currentUser;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var userName = _userAccessor.GetCurrentUsername();
            var user = await _userManager.FindByNameAsync(userName);
            _currentUser.User = user;
            return await next();
        }
    }
}