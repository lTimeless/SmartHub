using System.Threading.Tasks;

namespace SmartHub.Application.Common.Interfaces.Database
{
    public interface IDbSeeder
    {

        /// <summary>
        /// Adds some default values to the Db
        /// </summary>
        Task SeedData();
    }
}