using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SmartHub.Application.Common.Exceptions;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Domain;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.UseCases.Identity.Registration
{
	/// <inheritdoc cref="IRegistrationService"/>
	public class RegistrationService : IRegistrationService
	{
		private readonly IUserRepository _userRepository;
		private readonly IOptionsSnapshot<AppConfig> _appConfig;
		public RegistrationService(IUserRepository userRepository, IOptionsSnapshot<AppConfig> appConfig)
		{
			_userRepository = userRepository;
			_appConfig = appConfig;
		}

		/// <inheritdoc cref="IRegistrationService.RegisterAsync"/>
		public async Task<bool> RegisterAsync(RegistrationCommand userInput, User user)
		{
			if (_appConfig.Value.IsActive is false)
			{
				return false;
			}

			var created = await _userRepository.CreateUser(user, userInput.Password, userInput.Role);
			if (created)
			{
				return true;
			}
			throw new SmartHubException("Problem Registering new User.");
		}
	}
}
