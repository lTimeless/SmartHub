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
			File.WriteAllText(path, content);
			return true;
		}

		/// <inheritdoc cref="IFileService.ReadFileAsString(string)"/>
		public string ReadFileAsString(string path)
		{
			return File.ReadAllText(path);
		}
	}
}
