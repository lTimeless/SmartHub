using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using SmartHub.Application.Common.Extensions;
using System.Reflection;

namespace SmartHub.Application.Common.Attributes
{
	public class UseCurrentUserAttribute : ObjectFieldDescriptorAttribute
	{
		public override void OnConfigure(IDescriptorContext context, IObjectFieldDescriptor descriptor, MemberInfo member)
		{
			descriptor.UseCurrentUser();
		}
	}
}