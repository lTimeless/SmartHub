using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Serilog.Sinks.AspNetCore.SignalR.Interfaces;
using SmartHub.Application.UseCases.Entity.Activities;
using SmartHub.Application.UseCases.Entity.Devices;
using SmartHub.Application.UseCases.Entity.Groups;
using SmartHub.Domain;

namespace SmartHub.Application.UseCases.SignalR
{
	// Diese Funktionen werden vom Server aufgerufen um Daten an den Client zu schicken
	public interface IServerHubClient : IHub
    {
        /// <summary>
        /// Sends an activity object to the clients
        /// </summary>
        /// <param name="activityDto">The Activity to send</param>
        /// <returns>Task</returns>
        Task SendActivity(ActivityDto activityDto);

		// Task SendGroups(IEnumerable<GroupDto> homeDto);

		// Task SendDevices(IEnumerable<DeviceDto> homeDto);

		Task SendAppConfig(AppConfig appConfig);
	}
}