using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Common.Extensions;
using SmartHub.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.Entity.Groups.Mutations
{
	/// <summary>
	/// Endpoint for create group.
	/// </summary>
	[Authorize]
	[GraphQLDescription("Endpoint for create group.")]
	public class CreateGroupMutation
	{
		/// <summary>
		/// Create a new group.
		/// </summary>
		/// <param name="groupRepository">The group repository.</param>
		/// <param name="unitOfWork">The unit-of-work.</param>
		/// <param name="input">The group to create.</param>
		/// <returns></returns>
		public async Task<GroupPayload> CreateGroup([Service] IBaseRepositoryAsync<Group> groupRepository,
			[Service] IUnitOfWork unitOfWork, CreateGroupInput input)
		{
			if (string.IsNullOrWhiteSpace(input.Name))
			{
				return new GroupPayload(new UserError($"Group name can't be empty, null or whitespace: {input.Name}", AppErrorCodes.IsEmpty));
			}
			var groupsExist = groupRepository.GetAllAsQueryable().Any(x => x.Name == input.Name);
			if (groupsExist)
			{
				return new GroupPayload(new UserError($"Group with name {input.Name} already exist.", AppErrorCodes.Exists));
			}

			var newGroup = new Group(input.Name, input.Description);
			var created = await groupRepository.AddAsync(newGroup);
			if (created)
			{
				await unitOfWork.SaveAsync();
				// TODO hier dann über den TopicSender an eine Subscription senden
				return new GroupPayload(newGroup, $"Created new Group with name {input.Name}.");
			}
			return new GroupPayload(new UserError($" Couldn't create new Group with name {input.Name}", AppErrorCodes.NotCreated));
		}
	}
}