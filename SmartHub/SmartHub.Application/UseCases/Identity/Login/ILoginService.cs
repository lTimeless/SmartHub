using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.Identity.Login
{
	public interface ILoginService
	{
		Task<AuthResponseDto> LoginAsync(LoginQuery userInput);
	}
}
