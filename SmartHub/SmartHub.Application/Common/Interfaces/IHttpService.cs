using System.Threading.Tasks;

namespace SmartHub.Application.Common.Interfaces
{
	public interface IHttpService
	{
		Task<bool> SendAsync(string ipAddress, string query);
		Task GetAsync(string ipAddress, string query);
		Task PostAsync(string ipAddress, string query);
		Task PutAsync(string ipAddress, string query);
	}
}
