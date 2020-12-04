using Microsoft.AspNetCore.SignalR;
using SmartHub.Application.UseCases.SignalR.Services;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.SignalR
{
    // Diese Funktione werden vom Client aufgerufen um daten an den Server zu schicken
    // im client mit: connection.send(<..>,data)
    public class HomeHub : Hub<IServerHubClient>
    {
		private readonly ISendOverSignalR _sendOverSignalR;

		public HomeHub(ISendOverSignalR sendOverSignalR)
		{
			_sendOverSignalR = sendOverSignalR;
		}
		/// <summary>
		/// On connecting send the current home object to all users
		/// </summary>
		/// <returns>Task</returns>
		public override async Task OnConnectedAsync()
		{
			await _sendOverSignalR.SendDevices();
			await _sendOverSignalR.SendGroups();
			await _sendOverSignalR.SendAppConfig();
			await base.OnConnectedAsync();
		}
	}
}