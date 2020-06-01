using SmartHub.Application.Common.Interfaces.Events;

namespace SmartHub.Application.UseCases.Identity.Login
{
	public class LoginEvent : IApplicationEvent
	{
		public string UserName { get; }
		public string Successful { get; set; }

		public LoginEvent(string userName, bool successful)
		{
			UserName = userName;
			Successful = successful ? "Successful" : "Failed";
		}
	}
}
