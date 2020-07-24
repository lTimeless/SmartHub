using System;
using SmartHub.Application.Common.Interfaces.Events;
using SmartHub.Domain.Common.Enums;

namespace SmartHub.Application.UseCases.Identity.Login
{
	public class LoginEvent : ApplicationEvent
	{
		public string UserName { get; }
		public string Successful { get; }
		public override string EventType { get; }

		public LoginEvent(string userName, bool successful)
		{
			EventType = EventTypes.Login.ToString();
			UserName = userName;
			Successful = successful ? "Successful" : "Failed";
		}


	}
}
