using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using System.Reflection;

namespace SmartHub.Application.Common.Extensions
{
	public class UseCurrentUserAttribute : ObjectFieldDescriptorAttribute
	{
		public override void OnConfigure(IDescriptorContext context, IObjectFieldDescriptor descriptor, MemberInfo member)
		{
			descriptor.UseCurrentUser();
		}
	}

	/// <summary>
	/// Extension for ObjectFieldDescriptor to get the user from the jwt.
	/// </summary>
	public static class ObjectFieldDescriptorExtensions
	{
		public static IObjectFieldDescriptor UseCurrentUser(this IObjectFieldDescriptor descriptor)
		{
			return descriptor.Use(next => async context =>
			{
				var userAccessor = context.Services.GetService(typeof(IUserAccessor)) as IUserAccessor;
				var userRepo = context.Services.GetService(typeof(IUserRepository)) as IUserRepository;
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