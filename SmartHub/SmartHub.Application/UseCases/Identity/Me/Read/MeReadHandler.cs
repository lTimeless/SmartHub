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
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly CurrentUser _currentUser;

		public MeReadHandler(IUnitOfWork unitOfWork, IMapper mapper, CurrentUser currentUser)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_currentUser = currentUser;
		}

		public async Task<Response<UserDto>> Handle(MeReadQuery request, CancellationToken cancellationToken)
		{
			if (_currentUser.User == null)
			{
				return Response.Fail<UserDto>("Error: Something went wrong retrieving your account data", new UserDto());
			}
			var user = await _unitOfWork.UserRepository.GetUserByName(_currentUser.User.UserName);
			return Response.Ok(_mapper.Map<UserDto>(user));
		}
	}
}