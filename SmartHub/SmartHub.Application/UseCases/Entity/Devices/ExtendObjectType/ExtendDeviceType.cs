using HotChocolate.Types;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Entities;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.Entity.Devices.ExtendObjectType
{
	public class ExtendDeviceType : ObjectTypeExtension<Device>
	{
		protected override void Configure(IObjectTypeDescriptor<Device> descriptor)
		{
			descriptor.Field("status")
				.Type(typeof(DeviceStateResponse))
				.Resolve(async (x, c) =>
				{
					var t = Task.Run(() =>
					{
						Task.Delay(1000, c).Wait(c);
					});
					t.Wait(1000, c);
					//TODO: hier für das gerät den status via einem http call abfragen
					return new DeviceStateResponse(true, "",1,1,1,1);
				});
		}
	}
}