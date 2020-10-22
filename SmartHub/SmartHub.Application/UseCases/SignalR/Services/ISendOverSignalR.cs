using System.Threading.Tasks;
using SmartHub.Application.Common.Models;
using SmartHub.Application.UseCases.Entity.Activities;

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
		/// <param name="activityDto">The given activity object</param>
		/// <param name="userName"></param>
		/// <param name="requestName"></param>
		/// <param name="message"></param>
		/// <param name="execTime"></param>
		/// <param name="success"></param>
		/// <returns>Task state</returns>
		Task SendActivity(string userName,string requestName, string message ,long execTime, bool success);
	}
}
