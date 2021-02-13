using System;

namespace SmartHub.BasePlugin
{
	/// <summary>
	/// Interface for plugins.
	/// </summary>
    public interface IPlugin
    {
	    /// <summary>
	    /// Plugin Id.
	    /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Plugin Name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Company name to which the plugin belongs.
        /// </summary>
        string Company { get; set; }

        /// <summary>
        /// Last modified date.
        /// </summary>
        DateTime ModifiedAt { get; }

        /// <summary>
        /// Creation date.
        /// </summary>
        DateTime CreatedAt { get; }
        /// <summary>
        /// Build version of the Assembly.
        /// </summary>
        string AssemblyVersion { get; }
    }
}