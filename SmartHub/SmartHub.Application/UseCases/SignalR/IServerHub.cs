using System.Threading.Tasks;
using Serilog.Sinks.AspNetCore.SignalR.Interfaces;
using SmartHub.Application.UseCases.Entity.Activities;

namespace SmartHub.Application.UseCases.SignalR
{
	// Diese Funktionen werden vom Server aufgerufen um Daten an den Client zu schicken
	public interface IServerHub : IHub
    {
        /// <summary>
        /// Sends an activity object to the clients
        /// </summary>
        /// <param name="activityDto">The Activity to send</param>
        /// <returns>Task</returns>
        Task SendActivity(ActivityDto activityDto);
    }
}