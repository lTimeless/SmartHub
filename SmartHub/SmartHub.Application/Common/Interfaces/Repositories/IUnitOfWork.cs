using System;
using System.Threading.Tasks;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.Common.Interfaces.Repositories
{
	public interface IUnitOfWork : IDisposable
	{
		IHomeRepository HomeRepository { get; }
		IBaseRepository<Group> GroupRepository { get; }
		IUserRepository UserRepository { get; }

		Task SaveAsync();

		Task Rollback();
	}
}
