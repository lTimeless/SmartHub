using SmartHub.Domain.Entities;

namespace SmartHub.Application.Common.Models
{
    public class CurrentUser
    {
        /// <summary>
        /// The current logged in <see cref="User"/>
        /// </summary>
        public User? User { get; set; }
        /// <summary>
        /// This current RequesterName, this can also be "System".
        /// For the logic look into "UserAccessor"
        /// </summary>
        public string? RequesterName { get; set; }
    }
}