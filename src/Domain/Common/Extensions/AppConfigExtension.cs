using SmartHub.Domain.Entities.ValueObjects;

namespace SmartHub.Domain.Common.Extensions
{
	/// <summary>
	/// Extension methods for <see cref="AppConfig"/> entity.
	/// </summary>
	public static class AppConfigExtension
    {
		/// <summary>
		/// Adds an address to the applicationConfig.
		/// </summary>
		/// <param name="app"></param>
		/// <param name="city"></param>
		/// <param name="state"></param>
		/// <param name="country"></param>
		/// <param name="zipCode"></param>
		/// <param name="street"></param>
		/// <returns></returns>
		public static AppConfig AddAddress(this AppConfig app, string city, string state, string country, string zipCode, string street = "")
		{
			app.Address = new Address(street, city, state, country, zipCode);
			return app;
		}
    }
}