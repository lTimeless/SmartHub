using System.Threading.Tasks;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.UseCases.Identity.Login
{
	/// <summary>
	/// Service for logins
	/// </summary>
	public interface ILoginService
	{
		/// <summary>
		/// Handles the login process
		/// </summary>
		/// <param name="userInput">The input the user does</param>
		/// <returns>An AuthResponseDto</returns>
		Task<bool> LoginAsync(LoginQuery userInput, User foundUser);
	}
}
