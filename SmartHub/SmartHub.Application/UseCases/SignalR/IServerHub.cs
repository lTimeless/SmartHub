using System.Threading.Tasks;
using Serilog.Sinks.AspNetCore.SignalR.Interfaces;
using SmartHub.Application.Common.Models;
using SmartHub.Application.UseCases.Entity.Homes;

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

        /// <summary>
        /// Sends the homeDto
        /// </summary>
        /// <param name="homeDto">The home object to send</param>
        /// <returns>Task</returns>
        Task SendHome(HomeDto homeDto);

    }
}