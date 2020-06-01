using SmartHub.Domain.Entities;
using SmartHub.Domain.Entities.Groups;
using SmartHub.Domain.Entities.Homes;
using System;
using System.Threading.Tasks;

namespace SmartHub.Application.Common.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		IBaseRepository<Home> HomeRepository { get; }
		IBaseRepository<Group> GroupRepository { get; }

		Task SaveAsync();

		Task Rollback();
	}
}
