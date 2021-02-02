using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SmartHub.Application.Common.Interfaces
{
	public interface IHttpService
	{
		/// <summary>
		/// This Method will just send data to the endpoint and return true if the response was a '200 ok'
		/// false if it wasn't
		/// </summary>
		/// <param name="ipAddress">the address to send the data to</param>
		/// <param name="queryTuple">the data which is send</param>
		/// <returns>true is request was ok , false if it wasn't</returns>
		Task<Tuple<T?, bool>> SendAsync<T>(string ipAddress, Tuple<string, Dictionary<string, string?>> queryTuple)  where T: class;
		Task<T> GetAsync<T>(string ipAddress, string? scheme = "http", string? query = null);
		Task PostAsync(string ipAddress, string query);
		Task PutAsync(string ipAddress, string query);
	}
}
