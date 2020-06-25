using System.IO;
using SmartHub.Application.Common.Interfaces;

namespace SmartHub.Infrastructure.Services.FileSystem
{
    public class DirectoryService : IDirectoryService
    {
        public void CreateDirectory(string path, string folderName)
        {
            string folderPath = Path.Combine(path, folderName);
            // Ensure the directory and all its parents exist.
            // doesn't exist = create , does exist = nothing happens
            Directory.CreateDirectory(folderPath);
        }

        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

    }
}