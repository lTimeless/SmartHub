using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartHub.Application.Common.Exceptions;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Interfaces.Repositories;
using SmartHub.Application.Common.Utils;
using SmartHub.Domain.Entities;
using SmartHub.Domain.Entities.ValueObjects;
using SmartHub.Domain.Enums;
using DateTime = System.DateTime;

namespace SmartHub.Application.UseCases.Identity.Registration
{
	public class RegistrationService : IRegistrationService
	{
		private readonly UserManager<User> _userManager;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IdentityService _identityService;

		public RegistrationService(UserManager<User> userManager, IUnitOfWork unitOfWork, IdentityService identityService)
		{
			_userManager = userManager;
			_unitOfWork = unitOfWork;
			_identityService = identityService;
		}

		public async Task<AuthResponseDto> RegisterAsync(RegistrationCommand userInput)
		{
			var userFound = await _userManager.FindByNameAsync(userInput.Username);
			if (userFound != null)
			{
				throw new RestException(HttpStatusCode.BadRequest, new { Username = "Username already exists" });
			}
			var user = new User(userInput.Username, null, new PersonName("", "", ""), null);

			var created = await _unitOfWork.UserRepository.CreateUser(user, userInput.Password, userInput.Role);
			if (created)
			{
				return _identityService.CreateAuthResponse(user, new List<string> {userInput.Role});

			}
			throw new SmartHubException("Problem Registering new User");
		}
	}
}
