using HotChocolate;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Entities;
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
		public async Task<Response<string>> CreateDevice([Service] IBaseRepositoryAsync<Device> deviceRepository,
			[Service] IBaseRepositoryAsync<Group> groupRepository,
			[Service] IUnitOfWork unitOfWork,
			CreateDeviceInput input)
		{
			var foundDevice = await deviceRepository.FindByAsync(x => x.Name == input.Name);
			if (foundDevice is not null)
			{
				return Response.Fail($"Device with name {input.Name} already exists", "");

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
				return Response.Ok($"Created new Device with name {newDevice.Name}");
			}
			return Response.Fail($" Couldn't create new Device with name {newDevice.Name}", "");
		}

		/// <summary>
		/// Update given device.
		/// </summary>
		/// <param name="deviceRepository">The device repository.</param>
		/// <param name="unitOfWork">The unit-of-work.</param>
		/// <param name="input">The device to update.</param>
		/// <returns>Response with message.</returns>
		public async Task<Response<string>> updateDevice([Service] IBaseRepositoryAsync<Device> deviceRepository,
			[Service] IUnitOfWork unitOfWork,
			UpdateDeviceInput input)
		{
			var foundDevice = await deviceRepository.FindByAsync(x => x.Id == input.Id);
			if (foundDevice is null)
			{
				return Response.Fail($"Error: Couldn't find device with id {input.Id}.", string.Empty);
			}

			if (!string.IsNullOrEmpty(input.Name))
			{
				foundDevice.SetName(input.Name);
			}
			if (!string.IsNullOrEmpty(input.Description))
			{
				foundDevice.SetDescription(input.Description);
			}
			if (!string.IsNullOrEmpty(input.Ipv4))
			{
				foundDevice.SetIp(input.Ipv4);
			}
			foundDevice.SetConnectionTypes(input.PrimaryConnection, input.SecondaryConnection);
			// TODO hier dann über den TopicSender an eine Subscription senden
			await unitOfWork.SaveAsync();
			return Response.Ok($"Updated device with name {input.Name}");
		}
	}
}