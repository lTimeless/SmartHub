using SmartHub.Application.Common.Interfaces;
using System.IO;

namespace SmartHub.Infrastructure.Services.FileSystem
{
	public class FileService : IFileService
	{
		/// <inheritdoc cref="IFileService.CreateFile(string, string)"/>
		public void CreateFile(string path, string content)
		{
			File.WriteAllText(path, content);
		}

		/// <inheritdoc cref="IFileService.ReadFileAsString(string)"/>
		public string ReadFileAsString(string path)
		{
			return File.ReadAllText(path);
		}
	}
}
