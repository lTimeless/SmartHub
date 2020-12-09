using HotChocolate;

namespace SmartHub.Application.UseCases.Init
{
	/// <summary>
	/// Input to initialize the application.
	/// </summary>
	public record AppConfigInitInput (Optional<string> Name, Optional<string> Description, bool AutoDetectAddress);
}