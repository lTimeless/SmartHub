using System;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.HomeFolder
{
    /// <summary>
    /// Service for the SmartHub config folder
    /// </summary>
    public interface IHomeFolderService
    {
        /// <summary>
        /// Gets the home folder path and folder name.
        /// </summary>
        /// <returns>The folder path and name.</returns>
        Tuple<string, string> GetHomeFolderPath();

        /// <summary>
        /// Creates all folders and files for this project
        /// </summary>
        /// <returns>The state of the Task</returns>
        Task Create();
    }
}