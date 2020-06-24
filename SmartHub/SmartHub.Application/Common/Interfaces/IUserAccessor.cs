namespace SmartHub.Application.Common.Interfaces
{
	public interface IUserAccessor
	{
		/// <summary>
		/// Gets the current userName
		/// </summary>
		/// <returns>if no user is made the request the name 'Home' will be returned- because than the home did something automatically</returns>
		string GetCurrentUsername();

		/// <summary>
		/// Gets the current userId
		/// </summary>
		/// <returns>if no user made the request the id 'Home' will be returned- because than the home did something automatically</returns>
		string GetCurrentUserId();
	}
}
