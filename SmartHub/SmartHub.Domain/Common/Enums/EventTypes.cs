using System;

namespace SmartHub.Domain.Common.Enums
{
    [Flags]
    public enum EventTypes
    {
        None,
        Home,
        Http,
        Domain,
        Application,
        Login,
        Registration
    }
}