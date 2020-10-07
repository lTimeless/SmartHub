using System.Net.NetworkInformation;
using System.Threading.Tasks;
using SmartHub.Application.Common.Interfaces;

namespace SmartHub.Infrastructure.Services.Http
{
	/// <inheritdoc cref="IPingService"/>
	public class PingService : IPingService
	{
		/// <inheritdoc cref="IPingService.Ping"/>
		public async Task<PingReply> Ping(string ip, int timeout)
		{
			var ping = new Ping();
			var replay = await ping.SendPingAsync(ip, timeout).ConfigureAwait(false);
			ping.Dispose();
			return replay;
		}
	}
}
