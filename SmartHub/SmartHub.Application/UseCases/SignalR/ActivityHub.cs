using Microsoft.AspNetCore.SignalR;

namespace SmartHub.Application.UseCases.SignalR
{
    // Diese Funktione werden vom Client aufgerufen um daten an den Server zu schicken
    // im client mit: connection.send(<..>,data)
    public class ActivityHub : Hub<IServerHub>
    {

    }
}