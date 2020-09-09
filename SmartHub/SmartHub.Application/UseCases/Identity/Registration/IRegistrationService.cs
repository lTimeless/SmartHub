using System.Threading.Tasks;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.UseCases.Identity.Registration
{
	/// <summary>
	/// Service for registrations
	/// </summary>
	public interface IRegistrationService
	{
		/// <summary>
		/// Handles the registration process
		/// </summary>
		/// <param name="userInput">The input the user does</param>
		/// <param name="user">The new created user</param>
		/// <returns>A bool which indicates if the function was successful or not</returns>
		Task<bool> RegisterAsync(RegistrationCommand userInput, User user);
	}
}
