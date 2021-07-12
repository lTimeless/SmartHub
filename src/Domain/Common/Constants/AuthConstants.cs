namespace SmartHub.Domain.Common.Constants
{
	public static class AuthConstants
	{
		public static class AuthorizationPolicies
		{
			public const string Admin = nameof(Admin);
		}

		public static class CorsPolicies
		{
			public const string AllowAny = nameof(AllowAny);
			public const string Admin = nameof(Admin);
		}
	}
}