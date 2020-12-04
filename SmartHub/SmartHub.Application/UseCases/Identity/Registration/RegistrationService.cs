using System.Threading.Tasks;
using SmartHub.Application.Common.Exceptions;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.UseCases.Identity.Registration
{
	/// <inheritdoc cref="IRegistrationService"/>
	public class RegistrationService : IRegistrationService
	{
		private readonly IUserRepository _userRepository;

		public RegistrationService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		/// <inheritdoc cref="IRegistrationService.RegisterAsync"/>
		public async Task<bool> RegisterAsync(RegistrationCommand userInput, User user)
		{
			var created = await _userRepository.CreateUser(user, userInput.Password, userInput.Role);
			if (created)
			{
				return true;
			}
			throw new SmartHubException("Problem Registering new User.");
		}
	}
}
