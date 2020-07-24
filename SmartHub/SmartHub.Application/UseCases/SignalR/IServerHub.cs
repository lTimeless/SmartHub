using System.Threading.Tasks;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.DomainEvents;

namespace SmartHub.Application.UseCases.SignalR
{
    // Diese Funktionen werden vom Server aufgerufen um Daten an den Client zu schicken
    // im client mit: connection.on("<SendMessage>",data);
    public interface IServerHub
    {
        Task SendAsString(string eventMessage);
        Task SendEvent(IEvent eventObject);

        Task SendLog(ServerLog serverLog);
    }
}