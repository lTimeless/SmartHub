using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace SmartHub.Application.Common.Interfaces
{
	public interface IPingService
	{
		Task<PingReply> Ping(string ip, int timeout);
	}
}
