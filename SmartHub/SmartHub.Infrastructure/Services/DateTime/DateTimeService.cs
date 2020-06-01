using SmartHub.Application.Common.Interfaces;

namespace SmartHub.Infrastructure.Services.DateTime
{
    public class DateTimeService : IDateTimeService
    {
	    public System.DateTime GetNow()
		{
			return System.DateTime.Now;
		}
	}
}
