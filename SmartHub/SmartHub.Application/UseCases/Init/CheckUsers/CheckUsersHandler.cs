using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;

namespace SmartHub.Application.UseCases.Init.CheckUsers
{
	public class CheckUsersHandler : IRequestHandler<CheckUsersQuery, Response<bool>>
	{
		private readonly IUserRepository _userRepository;

		public CheckUsersHandler(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<Response<bool>> Handle(CheckUsersQuery request, CancellationToken cancellationToken)
		{
			var usersExist = await _userRepository.UsersExist().ConfigureAwait(false);
			return usersExist ? Response.Ok(true) : Response.Fail("No users exist.", false);
		}
	}
}