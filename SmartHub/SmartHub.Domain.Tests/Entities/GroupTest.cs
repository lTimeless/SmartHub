using System.Linq;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Entities;
using Xunit;

namespace SmartHub.Domain.Tests.Entities
{
    public class GroupTest
    {
        private const string Name = "GroupName";
        private const string Description = "GroupDescription";

        private readonly Group group;

        public GroupTest()
        {
            group = new Group(Name, Description);
        }

        [Fact]
        internal void SetName_ShouldChangeName()
        {
            const string newName = "another name";

            string actual = group.SetName(newName).Name;

            Assert.Equal(newName, actual);
        }

        [Fact]
        internal void SetName_SetDescription()
        {
            const string newDescription = "test";

            string? actual = group.SetDescription(newDescription).Description;

            Assert.Equal(newDescription, actual);
        }

        [Fact]
        internal void Devices_ByDefault_EmptyList()
        {
            Assert.Empty(group.Devices);
        }

        [Fact]
        internal void AddDevice_ShouldAddDeviceToCollection()
        {
            Device device = new Device("name", default, "ip", "company", ConnectionTypes.None, default, "pluginName", default);

            Device? addedDevice = group.AddDevice(device).Devices.FirstOrDefault();

            Assert.Same(device, addedDevice);
        }
    }
}