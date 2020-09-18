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
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly CurrentUser _currentUser;

		public MeUpdateHandler(IUnitOfWork unitOfWork, IMapper mapper, CurrentUser currentUser)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_currentUser = currentUser;
		}

		public async Task<Response<UserDto>> Handle(MeUpdateCommand request, CancellationToken cancellationToken)
		{
			if (string.IsNullOrEmpty(request.UserName))
			{
				return Response.Fail<UserDto>("Error: New username can't be empty.");
			}
			var userEntity = await _unitOfWork.UserRepository.GetUserByName(_currentUser.User.UserName);
			userEntity.UserName = request.UserName;
			userEntity.PersonInfo = request.PersonInfo;
			userEntity.PersonName.FirstName = request.FirstName;
			userEntity.PersonName.MiddleName = request.MiddleName;
			userEntity.PersonName.LastName = request.LastName;
			userEntity.Email = request.Email;
			userEntity.PhoneNumber = request.PhoneNumber;

			var updateUser = await _unitOfWork.UserRepository.UpdateUser(userEntity);
			if (!updateUser)
			{
				return Response.Fail<UserDto>($"Error: Something went wrong updating user {userEntity.UserName}.");
			}

			var currentRoles = await _unitOfWork.UserRepository.GetUserRoles(userEntity);
			// schaut ob die Liste nur einen Eintrag hat und dieser gleich meiner neuen Rolle ist
			if (!currentRoles.Except(new List<string> {request.NewRole}).Any())
			{
				return Response.Ok(_mapper.Map<UserDto>(userEntity));
			}

			var changeRole = await _unitOfWork.UserRepository.UserChangeRole(userEntity, request.NewRole);
			return changeRole
				? Response.Ok(_mapper.Map<UserDto>(userEntity))
				: Response.Fail<UserDto>($"Error: Something went wrong updating user and Role for {userEntity.UserName}.");
		}
	}
}
