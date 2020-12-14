namespace SmartHub.Application.UseCases.Entity.Users
{
	/// <summary>
	/// User update input.
	/// </summary>
	public record UpdateUserInput(string UserId,
		string? UserName,
		string? PersonInfo,
		string? FirstName,
		string? MiddleName,
		string? LastName,
		string? Email,
		string? PhoneNumber,
		string? NewRole);
}
