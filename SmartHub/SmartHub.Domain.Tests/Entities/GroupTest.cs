using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Common.Extensions;
using SmartHub.Domain.Entities;
using System.Linq;
using Xunit;

namespace SmartHub.Domain.Tests.Entities
{
    public class GroupTest
    {
        private const string _name = "GroupName";
        private const string _description = "GroupDescription";

        private readonly Group _group;

        public GroupTest()
        {
            _group = new Group(_name, _description);
        }

        [Fact]
        internal void SetName_ShouldChangeName()
        {
            const string newName = "another name";

            string actual = _group.SetName(newName).Name;

            Assert.Equal(newName, actual);
        }

        [Fact]
        internal void SetName_SetDescription()
        {
            const string newDescription = "test";

            string? actual = _group.SetDescription(newDescription).Description;

            Assert.Equal(newDescription, actual);
        }

        [Fact]
        internal void Devices_ByDefault_EmptyList()
        {
            Assert.Empty(_group.Devices);
        }

        [Fact]
        internal void AddDevice_ShouldAddDeviceToCollection()
        {
            Device device = new Device("name", default, "ip", "company", ConnectionTypes.None, default, "pluginName", default);

            Device? addedDevice = _group.AddDevice(device).Devices.FirstOrDefault();

            Assert.Same(device, addedDevice);
        }
    }
}