using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using SmartHub.Application.UseCases.Entity.Users;

namespace SmartHub.Application.UseCases.Identity.Me.Update
{
	public class MeUpdateHandler : IRequestHandler<MeUpdateCommand, Response<UserDto>>
	{
		private readonly IMapper _mapper;
		private readonly CurrentUser _currentUser;
		private readonly IUserRepository _userRepository;

		public MeUpdateHandler(IMapper mapper, CurrentUser currentUser, IUserRepository userRepository)
		{
			_mapper = mapper;
			_currentUser = currentUser;
			_userRepository = userRepository;
		}

		public async Task<Response<UserDto>> Handle(MeUpdateCommand request, CancellationToken cancellationToken)
		{
			if (string.IsNullOrEmpty(request.UserName))
			{
				return Response.Fail("Error: New username can't be empty.", new UserDto());
			}

			var userName = _currentUser.User?.UserName ?? string.Empty;
			var userEntity = await _userRepository.GetUserByName(userName);

			if (userEntity == null)
			{
				return Response.Fail($"Error: Something went wrong updating user {userName}.", new UserDto());
			}
			userEntity.UserName = request.UserName;
			userEntity.PersonInfo = request.PersonInfo;
			userEntity.PersonName.FirstName = request.FirstName;
			userEntity.PersonName.MiddleName = request.MiddleName;
			userEntity.PersonName.LastName = request.LastName;
			userEntity.Email = request.Email;
			userEntity.PhoneNumber = request.PhoneNumber;

			var updateUser = await _userRepository.UpdateUser(userEntity);
			if (!updateUser)
			{
				return Response.Fail($"Error: Something went wrong updating user {userEntity.UserName}.", new UserDto());
			}

			var currentRoles = await _userRepository.GetUserRoles(userEntity);
			// checks if th elist has an entry and if that one is equal to the new role
			if (!currentRoles.Except(new List<string> {request.NewRole}).Any())
			{
				return Response.Ok(_mapper.Map<UserDto>(userEntity));
			}

			var changeRole = await _userRepository.UserChangeRole(userEntity, request.NewRole);
			return changeRole
				? Response.Ok(_mapper.Map<UserDto>(userEntity))
				: Response.Fail($"Error: Something went wrong updating user and Role for {userEntity.UserName}.", new UserDto());
		}
	}
}
