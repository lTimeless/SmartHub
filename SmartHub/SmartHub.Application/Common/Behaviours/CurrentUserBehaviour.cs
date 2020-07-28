using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Interfaces.Repositories;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.Common.Behaviours
{
    public class CurrentUserBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
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

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var userName = _userAccessor.GetCurrentUsername();
            var user = await _unitOfWork.UserRepository.GetUserByName(userName);
            _currentUser.User = user;
            _currentUser.RequesterName = userName;
            return await next();
        }
    }
}