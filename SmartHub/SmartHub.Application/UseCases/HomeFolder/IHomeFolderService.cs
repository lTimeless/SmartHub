using System;
using SmartHub.Application.Common.Interfaces;

namespace SmartHub.Application.UseCases.HomeFolder
{
    /// <summary>
    /// Service for the SmartHub config folder
    /// </summary>
    public interface IHomeFolderService : IInitialize
    {
        /// <summary>
        /// Gets the home folder path and folder name.
        /// </summary>
        /// <returns>The folder path and name.</returns>
        Tuple<string, string> GetHomeFolderPath();
    }
}