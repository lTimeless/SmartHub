using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.Identity.Registration
{
	public interface IRegistrationService
	{
		Task<AuthResponseDto> RegisterAsync(RegistrationCommand userInput);
	}
}
