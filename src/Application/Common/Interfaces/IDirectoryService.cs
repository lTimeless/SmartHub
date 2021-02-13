namespace SmartHub.Application.Common.Interfaces
{
    public interface IDirectoryService
    {
        /// <summary>
        /// This creates a new directory or does nothing if it already exists
        /// </summary>
        /// <param name="path">The creation path</param>
        /// <param name="folderName">The name of the folder</param>
        void CreateDirectory(string path, string folderName);

        /// <summary>
        /// This creates a new directory or does nothing if it already exists
        /// </summary>
        /// <param name="path">The creation path</param>
        void CreateDirectory(string path);
    }
}