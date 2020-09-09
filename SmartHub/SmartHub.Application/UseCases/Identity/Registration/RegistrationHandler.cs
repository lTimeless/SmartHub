using System.Collections.Generic;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using SmartHub.Application.Common.Interfaces.Repositories;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Entities;
using SmartHub.Domain.Entities.ValueObjects;

namespace SmartHub.Application.UseCases.Identity.Registration
{
	public class RegistrationHandler : IRequestHandler<RegistrationCommand, Response<AuthResponseDto>>
	{
		private readonly IRegistrationService _registrationService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IdentityService _identityService;

		public RegistrationHandler(IRegistrationService registrationService, IUnitOfWork unitOfWork, IdentityService identityService)
		{
			_registrationService = registrationService;
			_unitOfWork = unitOfWork;
			_identityService = identityService;
		}

		public async Task<Response<AuthResponseDto>> Handle(RegistrationCommand request, CancellationToken cancellationToken)
		{
			var foundUser = await _unitOfWork.UserRepository.GetUserByName(request.Username);
			if (foundUser != null)
			{
				return Response.Fail<AuthResponseDto>("Username already exists.");
			}
			var newUser = new User(request.Username, null, new PersonName("", "", ""));
			var result = await _registrationService.RegisterAsync(request, newUser);

			return result
				? Response.Ok("Successful",
					_identityService.CreateAuthResponse(newUser, new List<string> {request.Role}))
				: Response.Fail<AuthResponseDto>($"Error: Could not register user {request.Username}");
		}
	}
}
