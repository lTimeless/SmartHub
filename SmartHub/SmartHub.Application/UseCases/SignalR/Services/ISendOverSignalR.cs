using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.SignalR.Services
{
	/// <summary>
	/// Here are functions for sending objects over signalR
	/// </summary>
	public interface ISendOverSignalR
	{
		/// <summary>
		/// Sends the current home
		/// </summary>
		/// <returns></returns>
		Task SendHome();
	}
}
