using System.Collections.Generic;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Entities;
using SmartHub.Application.UseCases.AppFolder.AppConfigParser;

namespace SmartHub.Application.UseCases.Identity.Registration
{
	public class RegistrationHandler : IRequestHandler<RegistrationCommand, Response<AuthResponseDto>>
	{
		private readonly IUserRepository _userRepository;
		private readonly IRegistrationService _registrationService;
		private readonly IdentityService _identityService;
		private readonly IAppConfigService _configService;

		public RegistrationHandler(IRegistrationService registrationService, IdentityService identityService, IUserRepository userRepository, IAppConfigService configService)
		{
			_registrationService = registrationService;
			_identityService = identityService;
			_userRepository = userRepository;
			_configService = configService;
		}

		public async Task<Response<AuthResponseDto>> Handle(RegistrationCommand request, CancellationToken cancellationToken)
		{
			if (_configService.GetConfig().IsActive is false)
			{
				return Response.Fail<AuthResponseDto>("There is no home created yet.", new AuthResponseDto(string.Empty));
			}

			var foundUser = await _userRepository.GetUserByName(request.Username);
			if (foundUser != null)
			{
				return Response.Fail("Username already exists.", new AuthResponseDto(string.Empty));
			}
			var newUser = new User(request.Username, "");
			var result = await _registrationService.RegisterAsync(request, newUser);

			return result
				? Response.Ok("Successful",
					_identityService.CreateAuthResponse(newUser, new List<string> {request.Role}))
				: Response.Fail($"Error: Could not register user {request.Username}", new AuthResponseDto(string.Empty));
		}
	}
}
