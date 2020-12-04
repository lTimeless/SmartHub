using SmartHub.Application.Common.Exceptions;
using SmartHub.Application.Common.Interfaces;
using System.IO;

namespace SmartHub.Infrastructure.Services.FileSystem
{
	public class FileService : IFileService
	{
		/// <inheritdoc cref="IFileService.CreateFile(string, string)"/>
		public bool CreateFile(string path, string content)
		{
			if (File.Exists(path))
			{
				return false;
			}

			try
			{
				File.WriteAllText(path, content);
			}
			catch (System.Exception ex)
			{
				throw new SmartHubException($"Error while writing to file {path}. Exception: {ex}");
			}
			return true;
		}

		public bool OverrideFile(string path, string content)
		{
			try
			{
				File.WriteAllText(path, content);
			}
			catch (System.Exception ex)
			{
				throw new SmartHubException($"Error while writing to file {path}. Exception: {ex}");
			}
			return true;
		}

		/// <inheritdoc cref="IFileService.ReadFileAsString(string)"/>
		public string ReadFileAsString(string path)
		{
			return File.ReadAllText(path);
		}
	}
}
