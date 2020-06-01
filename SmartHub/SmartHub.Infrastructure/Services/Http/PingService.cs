using SmartHub.Application.Common.Interfaces;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace SmartHub.Infrastructure.Services.Http
{
	public class PingService : IPingService
	{
		public async Task<PingReply> Ping(string ip, int timeout)
		{
			var ping = new Ping();
			var replay = await ping.SendPingAsync(ip, timeout).ConfigureAwait(false);
			ping.Dispose();
			return replay;
		}
	}
}
