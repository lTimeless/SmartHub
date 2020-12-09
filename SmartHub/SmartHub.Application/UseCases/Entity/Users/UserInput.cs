using HotChocolate;

namespace SmartHub.Application.UseCases.Entity.Users
{
	/// <summary>
	/// User update input.
	/// </summary>
	public record UpdateUserInput(string UserId,
		Optional<string> UserName,
		Optional<string> PersonInfo,
		Optional<string> FirstName,
		Optional<string> MiddleName,
		Optional<string> LastName,
		Optional<string> Email,
		Optional<string> PhoneNumber,
		Optional<string> NewRole);
}
