using System;

namespace SmartHub.Domain.Common.Enums
{
    /// <summary>
    /// The channel names you can
    /// </summary>
    [Flags]
    public enum ChannelNames
    {
        /// <summary>
        /// All Events belonging to the project layers -> Api, Domain, Application, Persistence
        /// </summary>
        System
    }
}