using System;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.AppFolder
{
	/// <summary>
	/// MainService for the spplication folders.
	/// Creates all folders and files on startup.
	/// </summary>
	public interface IAppFolderService
	{
		/// <summary>
		/// Gets the home folder path and folder name.
		/// </summary>
		/// <returns>The folder path and name.</returns>
		Tuple<string, string> GetHomeFolderPath();

		/// <summary>
		/// Creates all folders and files for this project.
		/// </summary>
		/// <returns>The state of the Task.</returns>
		Task Create();

		/// <summary>
		/// Saves all relevant data to the App base folders.
		/// </summary>
		/// <returns>The state of the Task.</returns>
		Task Save();
	}
}