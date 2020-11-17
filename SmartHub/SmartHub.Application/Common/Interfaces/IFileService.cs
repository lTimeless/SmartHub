﻿namespace SmartHub.Application.Common.Interfaces
{
	public interface IFileService
	{
		/// <summary>
		/// Create new File if it doesn't exist
		/// Overrides the file if it exists
		/// </summary>
		/// <param name="path">Path where to create the file</param>
		/// <param name="content">Content to write into the file</param>
		void CreateFile(string path, string content);

		/// <summary>
		/// Reads the file to the given path and return it as a string
		/// </summary>
		/// <param name="path">Path to the file</param>
		/// <returns>File content as string</returns>
		string ReadFileAsString(string path);
	}
}
