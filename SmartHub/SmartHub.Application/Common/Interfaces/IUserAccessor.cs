namespace SmartHub.Application.Common.Interfaces
{
	public interface IUserAccessor
	{
		/// <summary>
		/// Gets the current user
		/// </summary>
		/// <returns>if no user is active the name 'Home' will be returned- because than the home did something automatically</returns>
		string GetCurrentUsername();
	}
}
