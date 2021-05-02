using System.Collections.Generic;

namespace SmartHub.Domain.Common.Options
{
    /// <summary>
    /// The dynamic response compression options for the application.
    /// </summary>
    public class CompressionOptions
    {
        public CompressionOptions() => this.MimeTypes = new();

        /// <summary>
        /// Gets a list of MIME types to be compressed in addition to the default set used by ASP.NET Core.
        /// </summary>
        public List<string> MimeTypes { get; }
    }
}
