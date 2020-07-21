using SmartHub.Domain.Entities;
using SmartHub.Domain.Entities.ValueObjects;

namespace SmartHub.Domain.DomainEvents
{
	public class UserChangedEvent : IDomainEvent
	{
		public string UserId { get; }
		public string? PersonInfo { get; }

		public string? PasswordHash { get; }
		public bool? EmailConfirmed { get; }
		public string? Email { get; }
		public string? UserName { get; }

		public PersonName? PersonName { get; }

		public Home? Home { get; }

		public UserChangedEvent(string id, string? info, string? pw, bool? emailConf, string? email, string? userName, PersonName? personName, Home? home)
		{
			UserId = id;
			PersonInfo = info;
			PasswordHash = pw;
			EmailConfirmed = emailConf;
			Email = email;
			UserName = userName;
			PersonName = personName;
			Home = home;
		}
	}
}
