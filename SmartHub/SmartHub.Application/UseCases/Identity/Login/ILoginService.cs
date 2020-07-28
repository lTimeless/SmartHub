using System.Threading.Tasks;

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
		Task<AuthResponseDto> LoginAsync(LoginQuery userInput);
	}
}
