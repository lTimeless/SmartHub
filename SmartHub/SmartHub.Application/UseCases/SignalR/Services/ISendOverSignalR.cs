using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.SignalR.Services
{
	/// <summary>
	/// Here are functions for sending objects over signalR
	/// </summary>
	public interface ISendOverSignalR
	{
		/// <summary>
		/// Sends an activity object -> it is the current request but with only
		/// the needed info
		/// </summary>
		/// <param name="userName"></param>
		/// <param name="requestName"></param>
		/// <param name="message"></param>
		/// <param name="execTime"></param>
		/// <param name="success"></param>
		/// <returns>Task state</returns>
		Task SendActivity(string userName,string requestName, string message ,long execTime, bool success);
	}
}
