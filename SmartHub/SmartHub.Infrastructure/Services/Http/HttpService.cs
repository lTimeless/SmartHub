using SmartHub.Application.Common.Interfaces;
using System.Threading.Tasks;

namespace SmartHub.Infrastructure.Services.Http
{
	public class HttpService : IHttpService
	{
		public Task<bool> SendAsync(string ipAddress, string query)
		{
			//TODO: bsp. http://192.168.2.23?turn=On
			throw new System.NotImplementedException();
		}

		public Task GetAsync(string ipAddress, string query)
		{
			throw new System.NotImplementedException();
		}

		public Task PostAsync(string ipAddress, string query)
		{
			throw new System.NotImplementedException();
		}

		public Task PutAsync(string ipAddress, string query)
		{
			throw new System.NotImplementedException();
		}
	}
}
