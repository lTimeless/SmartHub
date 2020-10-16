using System.Threading.Tasks;
using SmartHub.Application.Common.Models;

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
		/// <returns>Task state</returns>
		Task SendHome();

		/// <summary>
		/// Sends an activity object -> it is the current request but with only
		/// the needed info
		/// </summary>
		/// <param name="activity">The given activity object</param>
		/// <returns>Task state</returns>
		Task SendActivity(Activity activity);
	}
}
