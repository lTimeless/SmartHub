using HotChocolate;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Entities;
using SmartHub.Domain.Entities.ValueObjects;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.Entity.Devices
{
	/// <summary>
	/// Endpoint for all device mutations.
	/// </summary>
	public class DeviceMutations
	{
		/// <summary>
		/// Create a new device.
		/// </summary>
		/// <param name="deviceRepository">The device repository.</param>
		/// <param name="groupRepository">The group repository.</param>
		/// <param name="unitOfWork">The unit-of-work.</param>
		/// <param name="input">The device to create.</param>
		/// <returns>Response with message.</returns>
		public async Task<DevicePayload> CreateDevice([Service] IBaseRepositoryAsync<Device> deviceRepository,
			[Service] IBaseRepositoryAsync<Group> groupRepository,
			[Service] IUnitOfWork unitOfWork,
			CreateDeviceInput input)
		{
			var foundDevice = await deviceRepository.FindByAsync(x => x.Name == input.Name);
			if (foundDevice is not null)
			{
				return new DevicePayload(new UserError($"Device with name {input.Name} already exists", AppErrorCodes.NotFound));
			}

			var newDevice = new Device(input.Name, input.Description, input.Ipv4, input.CompanyName,
				input.PrimaryConnection, input.SecondaryConnection,
				input.PluginName, input.PluginTypes);

			if (!string.IsNullOrEmpty(input.GroupName))
			{
				var foundGroup = await groupRepository.FindByAsync(x => x.Name == input.GroupName);
				if (foundGroup is not null)
				{
					foundGroup.AddDevice(newDevice);
				}
			}
			var created = await deviceRepository.AddAsync(newDevice);
			if (created)
			{
				await unitOfWork.SaveAsync();
				// TODO hier dann über den TopicSender an eine Subscription senden
				return new DevicePayload(newDevice, $"Created new Device with name {newDevice.Name}");
			}
			return new DevicePayload(new UserError($" Couldn't create new Device with name {newDevice.Name}", AppErrorCodes.NotCreated));
		}

		/// <summary>
		/// Update given device.
		/// </summary>
		/// <param name="deviceRepository">The device repository.</param>
		/// <param name="unitOfWork">The unit-of-work.</param>
		/// <param name="input">The device to update.</param>
		/// <returns>Response with message.</returns>
		public async Task<DevicePayload> updateDevice([Service] IBaseRepositoryAsync<Device> deviceRepository,
			[Service] IUnitOfWork unitOfWork,
			UpdateDeviceInput input)
		{
			var foundDevice = await deviceRepository.FindByAsync(x => x.Id == input.Id);
			if (foundDevice is null)
			{
				return new DevicePayload(new UserError($"Error: Couldn't find device with id {input.Id}.", AppErrorCodes.NotFound));
			}

			foundDevice.Name = !string.IsNullOrEmpty(input.Name) ? input.Name : foundDevice.Name;
			foundDevice.Description = !string.IsNullOrEmpty(input.Description) ? input.Description : foundDevice.Description;
			foundDevice.Ip = !string.IsNullOrEmpty(input.Ipv4)? new IpAddress(input.Ipv4): foundDevice.Ip ;
			foundDevice.PrimaryConnection = input.PrimaryConnection ?? foundDevice.PrimaryConnection ;
			foundDevice.SecondaryConnection = input.SecondaryConnection ?? foundDevice.SecondaryConnection ;

			await unitOfWork.SaveAsync();
			// TODO hier dann über den TopicSender an eine Subscription senden
			return new DevicePayload(foundDevice, $"Updated device with name {input.Name}");
		}
	}
}