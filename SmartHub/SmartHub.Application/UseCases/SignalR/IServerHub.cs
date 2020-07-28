using System.Threading.Tasks;
using Serilog.Sinks.AspNetCore.SignalR.Interfaces;
using SmartHub.Domain.DomainEvents;

namespace SmartHub.Application.UseCases.SignalR
{
    // Diese Funktionen werden vom Server aufgerufen um Daten an den Client zu schicken
    // im client mit: connection.on("<SendMessage>",data);
    public interface IServerHub : IHub
    {
        /// <summary>
        /// Sends an event to the clients
        /// </summary>
        /// <param name="eventObject">The event to send.</param>
        /// <returns>Task.</returns>
        Task SendEvent(IEvent eventObject);

    }
}