namespace SmartHub.Application.Common.Interfaces
{
	public interface IUserAccessor
	{
		/// <summary>
		/// Gets the current userName
		/// </summary>
		/// <returns>if no user is made the request the name 'Home' will be returned- because than the home did something automatically</returns>
		string GetCurrentUsername();
	}
}
