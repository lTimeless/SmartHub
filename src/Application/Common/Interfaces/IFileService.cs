namespace SmartHub.Application.Common.Interfaces
{
	public interface IFileService
	{
		/// <summary>
		/// Create new File if it doesn't exist and return true, else it returns false
		/// </summary>
		/// <param name="path">Path where to create the file.</param>
		/// <param name="content">Content to write into the file.</param>
		/// <returns>Returns true if file is created, returns false if file exists.</returns>
		bool CreateFile(string path, string content);

		/// <summary>
		/// Overide the File if it exists and return true, else it returns false
		/// </summary>
		/// <param name="path">Path where to create the file.</param>
		/// <param name="content">Content to write into the file.</param>
		/// <returns>Returns true if file is created, returns false if file exists.</returns>
		bool OverrideFile(string path, string content);

		/// <summary>
		/// Reads the file to the given path and returns it as a string.
		/// </summary>
		/// <param name="path">Path to the file.</param>
		/// <returns>File content as string.</returns>
		string ReadFileAsString(string path);
	}
}
