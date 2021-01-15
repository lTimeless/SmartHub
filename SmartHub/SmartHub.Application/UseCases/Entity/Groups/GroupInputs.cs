namespace SmartHub.Application.UseCases.Entity.Groups
{
	/// <summary>
	/// Group create input.
	/// </summary>
	public record CreateGroupInput(string Name, string? Description);

	/// <summary>
	/// Group update input.
	/// </summary>
	public record UpdateGroupInput(string Id, string? Name, string? Description);
}