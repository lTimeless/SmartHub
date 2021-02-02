using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Types;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Entities;
using SmartHub.Domain.Entities.ValueObjects;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.Entity.Devices.Mutations
{
	/// <summary>
	/// Endpoint for update device.
	/// </summary>
	[Authorize]
	[ExtendObjectType(Name = "AppMutations")]
	[GraphQLDescription("Endpoint for update device.")]

	public class UpdateDeviceMutation
	{
		/// <summary>
		/// Update given device.
		/// </summary>
		/// <param name="deviceRepository">The device repository.</param>
		/// <param name="unitOfWork">The unit-of-work.</param>
		/// <param name="input">The device to update.</param>
		/// <returns>Response with message.</returns>
		public async Task<DevicePayload> UpdateDevice([Service] IBaseRepositoryAsync<Device> deviceRepository,
			[Service] IUnitOfWork unitOfWork,
			UpdateDeviceInput input)
		{
			var foundDevice = await deviceRepository.FindByAsync(x => x.Id == input.Id);
			if (foundDevice == null)
			{
				return new DevicePayload(new UserError($"Error: Couldn't find device with id {input.Id}.", AppErrorCodes.NotFound));
			}

			foundDevice.Name = string.IsNullOrWhiteSpace(input.Name) ? foundDevice.Name : input.Name;
			foundDevice.Description = string.IsNullOrWhiteSpace(input.Description) ? foundDevice.Description : input.Description;
			foundDevice.Ip = string.IsNullOrWhiteSpace(input.Ipv4) ? foundDevice.Ip : new IpAddress(input.Ipv4);
			foundDevice.PrimaryConnection = input.PrimaryConnection ?? foundDevice.PrimaryConnection ;
			foundDevice.SecondaryConnection = input.SecondaryConnection ?? foundDevice.SecondaryConnection ;

			await unitOfWork.SaveAsync();
			// TODO hier dann über den TopicSender an eine Subscription senden
			return new DevicePayload(foundDevice, $"Updated device with name {input.Name}");
		}
	}
}