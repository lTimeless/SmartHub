using AutoMapper;
using MediatR;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.Entity.Users.Put
{
	public class UserPutHandler : IRequestHandler<UserPutCommand, Response<UserDto>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly CurrentUser _currentUser;

		public UserPutHandler(IUnitOfWork unitOfWork, IMapper mapper, CurrentUser currentUser)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_currentUser = currentUser;
		}

		public async Task<Response<UserDto>> Handle(UserPutCommand request, CancellationToken cancellationToken)
		{
			var userEntity = await _unitOfWork.UserRepository.GetUserByName(_currentUser.User.UserName);
			userEntity.UserName = request.UserName;
			userEntity.PersonInfo = request.PersonInfo;
			userEntity.PersonName.FirstName = request.FirstName;
			userEntity.PersonName.MiddleName = request.MiddleName;
			userEntity.PersonName.LastName = request.LastName;
			userEntity.Email = request.Email;
			userEntity.PhoneNumber = request.PhoneNumber;

			var result = await _unitOfWork.UserRepository.UpdateUser(userEntity);
			return !result
				? Response.Fail<UserDto>($"Error: Something went wrong updating user {userEntity.UserName}.")
				: Response.Ok(_mapper.Map<UserDto>(userEntity));
		}
	}
}
