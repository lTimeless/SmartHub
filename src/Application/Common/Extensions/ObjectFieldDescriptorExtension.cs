using HotChocolate.Types;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Models;

namespace SmartHub.Application.Common.Extensions
{
	public static class ObjectFieldDescriptorExtension
	{
		/// <summary>
		/// Extension for ObjectFieldDescriptor to get the user from the jwt.
		/// </summary>
		public static IObjectFieldDescriptor UseCurrentUser(this IObjectFieldDescriptor descriptor)
		{
			return descriptor.Use(next => async context =>
			{
				var userAccessor = context.Services.GetService(typeof(IUserAccessor)) as IUserAccessor;
				var userRepo = context.Services.GetService(typeof(IIdentityService)) as IIdentityService;
				var currentUser = context.Services.GetService(typeof(CurrentUser)) as CurrentUser ?? new CurrentUser();

				var userName = userAccessor!.GetCurrentUsername();
				var user = await userRepo!.FindByNameAsync(userName);

				currentUser.User = user;
				currentUser.RequesterName = userName;

				await next(context);
			});
		}
	}
}