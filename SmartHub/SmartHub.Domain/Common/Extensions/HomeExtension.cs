using System.Linq;
using SmartHub.Domain.Entities;

namespace SmartHub.Domain.Common.Extensions
{
    public static class HomeExtension
    {
        public static Home RemoveActivitiesOverLimit(this Home home, int saveXLimit, int deleteXAmount)
        {
            var amount = home.Activities.Count;
            deleteXAmount += amount - saveXLimit;
            var lastActivities = home.Activities
                .OrderByDescending(x => x.CreatedAt)
                .TakeLast(deleteXAmount)
                .ToList();
            home.Activities.RemoveAll(a => lastActivities.Exists(la => la.Id == a.Id));
            return home;
        }
    }
}