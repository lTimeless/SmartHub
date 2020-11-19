using System.Linq;
using SmartHub.Domain.Entities;
using SmartHub.Domain.Entities.ValueObjects;

namespace SmartHub.Domain.Common.Extensions
{
    public static class AppConfigExtension
    {
		public static AppConfig AddAddress(this AppConfig app, string city, string state, string country, string zipCode, string street = "")
		{
			app.Address = new Address(street, city, state, country, zipCode);
			return app;
		}


		/// <summary>
		/// Removes all and some Activities that are over the given list limit.
		/// </summary>
		/// <param name="home">The given Home.</param>
		/// <param name="saveXLimit">The list limit.</param>
		/// <param name="deleteXAmount">The amount to delete.</param>
		/// <returns></returns>
		public static AppConfig RemoveActivitiesOverLimit(this AppConfig home, int saveXLimit, int deleteXAmount)
        {
            //var amount = home.Activities.Count;
            //deleteXAmount += amount - saveXLimit;
            //var lastActivities = home.Activities
            //    .OrderByDescending(x => x.CreatedAt)
            //    .TakeLast(deleteXAmount)
            //    .ToList();
            //home.Activities.RemoveAll(a => lastActivities.Exists(la => la.Id == a.Id));
            return home;
        }

        /// <summary>
        /// Finds a device in all Groups and subgroups.
        /// </summary>
        /// <param name="home">The given Home.</param>
        /// <param name="deviceId">The id th look for.</param>
        /// <returns></returns>
        public static Device? FindDevice(this AppConfig home, string deviceId)
        {
			//var foundDevice = home.Groups.SelectMany(d => d.Devices)
			//        .ToList()
			//        .Find(x => x.Id == deviceId)
			//    ?? home.Groups.SelectMany(x => x.SubGroups).SelectMany(x => x.Devices)
			//        .ToList()
			//        .Find(c => c.Id == deviceId);
			//return foundDevice;
			return null;
        }
    }
}