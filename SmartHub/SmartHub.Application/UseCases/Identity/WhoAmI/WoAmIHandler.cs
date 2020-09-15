using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using SmartHub.Application.UseCases.Entity.Users;

namespace SmartHub.Application.UseCases.Identity.WhoAmI
{
    public class WoAmIHandler : IRequestHandler<WhoAmIQuery, Response<UserDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly CurrentUser _currentUser;

        public WoAmIHandler(IUnitOfWork unitOfWork, IMapper mapper, CurrentUser currentUser)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<Response<UserDto>> Handle(WhoAmIQuery request, CancellationToken cancellationToken)
        {
            if (_currentUser.User == null)
            {
                return Response.Fail<UserDto>("Error: Something went wrong retrieving your account data");
            }
            var user = await _unitOfWork.UserRepository.GetUserByName(_currentUser.User.UserName);
            return Response.Ok(_mapper.Map<UserDto>(user));
        }
    }
}