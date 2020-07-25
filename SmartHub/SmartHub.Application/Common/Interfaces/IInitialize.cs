using System.Threading.Tasks;

namespace SmartHub.Application.Common.Interfaces
{
    public interface IInitialize
    {
        /// <summary>
        /// Basic Init function for all children of this interface
        /// </summary>
        /// <returns>Task.</returns>
        Task Init()
        {
            return Task.CompletedTask;
        }
    }
}