using System.Threading.Tasks;

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
		/// <returns>An AuthResponseDto</returns>
		Task<AuthResponseDto> RegisterAsync(RegistrationCommand userInput);
	}
}
