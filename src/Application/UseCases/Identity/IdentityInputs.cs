namespace SmartHub.Application.UseCases.Identity
{
	/// <summary>
	///	Login input.
	/// </summary>
	public record LoginInput(string UserName, string Password);

	/// <summary>
	/// Registration input.
	/// </summary>
	public record RegistrationInput(string UserName, string Password, string Role);
}