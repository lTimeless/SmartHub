namespace SmartHub.Application.Common.Interfaces
{
	/// <summary>
	/// Service for retrieving information about the current user from the HttpContext.
	/// </summary>
	public interface ICurrentUserService
	{
		/// <summary>
		/// Gets the current userName.
		/// </summary>
		/// <returns>If no user is made the request the name 'Home' will be returned- because than the home did something automatically.</returns>
		string? GetCurrentUsername();
	}
}
