using System.Threading.Tasks;
using Serilog.Sinks.AspNetCore.SignalR.Interfaces;
using SmartHub.Application.UseCases.Entity.Homes;
using SmartHub.Domain.DomainEvents;

namespace SmartHub.Application.UseCases.SignalR
{
    // Diese Funktionen werden vom Server aufgerufen um Daten an den Client zu schicken
    // im client mit: connection.on("<SendEvent>",data);
    public interface IServerHub : IHub
    {
        /// <summary>
        /// Sends an event to the clients
        /// </summary>
        /// <param name="eventObject">The event to send</param>
        /// <returns>Task</returns>
        Task SendEvent(IBaseEvent eventObject);

        /// <summary>
        /// Sends the homeDto
        /// </summary>
        /// <param name="homeDto">The home object to send</param>
        /// <returns>Task</returns>
        Task SendHome(HomeDto homeDto);

    }
}