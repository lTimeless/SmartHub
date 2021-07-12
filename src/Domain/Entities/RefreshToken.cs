using System;

namespace SmartHub.Domain.Entities
{
	public class RefreshToken
	{
		public string Token { get; set; }
		public string JwtId { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime ExpirationDate { get; set; }
		public bool Used { get; set; }
		public bool Invalidated { get; set; }
		public string UserId { get; set; }
		public virtual User User { get; set; }
	}
}