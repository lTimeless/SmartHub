using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace SmartHub.Application.Common.Interfaces
{
	public interface IPingService
	{
		/// <summary>
		/// Send a ping to the given ip and returns it reply
		/// </summary>
		/// <param name="ip">The ip to send to.</param>
		/// <param name="timeout">The timeout.</param>
		/// <returns>The pingReply object.</returns>
		Task<PingReply> Ping(string ip, int timeout);
	}
}
