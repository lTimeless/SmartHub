using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Common.Extensions;
using SmartHub.Domain.Entities;
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

			if (input.IsSubGroup && !string.IsNullOrWhiteSpace(input.ParentGroupId))
			{
				var foundGroup = await groupRepository.FindByAsync(x => x.Id == input.ParentGroupId);
				if (foundGroup != null && foundGroup.IsSubGroup)
				{
					return new GroupPayload(new UserError("You can not create a subgroup of a subgroup.", AppErrorCodes.IsSubGroup));
				}

				var newSubgroup = new Group(input.Name, input.Description, input.IsSubGroup);
				foundGroup?.AddSubGroup(newSubgroup);
				await unitOfWork.SaveAsync();

				return new GroupPayload(newSubgroup, $"Created new SubGroup with name {input.Name} for group {foundGroup?.Name}.");
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