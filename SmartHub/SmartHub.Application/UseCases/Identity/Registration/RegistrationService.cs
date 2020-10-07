using System.Threading.Tasks;
using SmartHub.Application.Common.Exceptions;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Entities;
using SmartHub.Domain.Entities.ValueObjects;

namespace SmartHub.Application.UseCases.Identity.Registration
{
	/// <inheritdoc cref="IRegistrationService"/>
	public class RegistrationService : IRegistrationService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IChannelManager _channelManager;

		public RegistrationService( IUnitOfWork unitOfWork, IChannelManager channelManager)
		{
			_unitOfWork = unitOfWork;
			_channelManager = channelManager;
		}

		/// <inheritdoc cref="IRegistrationService.RegisterAsync"/>
		public async Task<bool> RegisterAsync(RegistrationCommand userInput, User user)
		{
			var homeEntity = await _unitOfWork.HomeRepository.GetHome();
			if (homeEntity is null)
			{
				return false;
			}
			var created = await _unitOfWork.UserRepository.CreateUser(user, userInput.Password, userInput.Role);
			if (created)
			{
				homeEntity.AddUser(user);
				await _channelManager.PublishNextToChannel(ChannelNames.System,
					new IdentityEvent(user.UserName,
						created,
						EventTypes.Registration));
				return true;
			}
			throw new SmartHubException("Problem Registering new User.");
		}
	}
}
