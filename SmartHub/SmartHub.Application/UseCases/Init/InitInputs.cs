using HotChocolate;

namespace SmartHub.Application.UseCases.Init
{
	/// <summary>
	/// Input to initialize the application.
	/// </summary>
	public record AppConfigInitInput (string? Name, string? Description, bool AutoDetectAddress);
}