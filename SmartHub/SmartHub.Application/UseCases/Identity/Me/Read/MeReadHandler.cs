using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using SmartHub.Application.UseCases.Entity.Users;

namespace SmartHub.Application.UseCases.Identity.Me.Read
{
	public class MeReadHandler : IRequestHandler<MeReadQuery, Response<UserDto>>
	{
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;
		private readonly CurrentUser _currentUser;

		public MeReadHandler(IMapper mapper, CurrentUser currentUser, IUserRepository userRepository)
		{
			_mapper = mapper;
			_currentUser = currentUser;
			_userRepository = userRepository;
		}

		public async Task<Response<UserDto>> Handle(MeReadQuery request, CancellationToken cancellationToken)
		{
			if (_currentUser.User == null)
			{
				return Response.Fail("Error: Something went wrong retrieving your account data", new UserDto());
			}
			var user = await _userRepository.GetUserByName(_currentUser.User.UserName);
			return Response.Ok(_mapper.Map<UserDto>(user));
		}
	}
}