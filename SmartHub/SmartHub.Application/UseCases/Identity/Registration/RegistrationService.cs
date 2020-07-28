using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartHub.Application.Common.Exceptions;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Interfaces.Repositories;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Entities;
using SmartHub.Domain.Entities.ValueObjects;

namespace SmartHub.Application.UseCases.Identity.Registration
{
	/// <inheritdoc cref="IRegistrationService"/>
	public class RegistrationService : IRegistrationService
	{
		private readonly UserManager<User> _userManager;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IdentityService _identityService;
		private readonly IChannelManager _channelManager;

		public RegistrationService(UserManager<User> userManager, IUnitOfWork unitOfWork, IdentityService identityService, IChannelManager channelManager)
		{
			_userManager = userManager;
			_unitOfWork = unitOfWork;
			_identityService = identityService;
			_channelManager = channelManager;
		}

		/// <inheritdoc cref="IRegistrationService.RegisterAsync"/>
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
				await _channelManager.PublishNextToChannel(EventTypes.Registration, new RegistrationEvent(user.UserName, created));
				return _identityService.CreateAuthResponse(user, new List<string> {userInput.Role});

			}
			throw new SmartHubException("Problem Registering new User");
		}
	}
}
