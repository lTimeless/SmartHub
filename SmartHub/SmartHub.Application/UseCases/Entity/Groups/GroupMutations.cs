using HotChocolate;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Entities;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.Entity.Groups
{
	/// <summary>
	/// Endpoint for all group mutations.
	/// </summary>
	public class GroupMutations
	{
		/// <summary>
		/// Method to Create a new group.
		/// </summary>
		/// <param name="groupRepository">The group repository.</param>
		/// <param name="unitOfWork">The unit-of-work.</param>
		/// <param name="input">The group to create.</param>
		/// <returns></returns>
		public async Task<GroupPayload> CreateGroup([Service] IBaseRepositoryAsync<Group> groupRepository,
			[Service] IUnitOfWork unitOfWork, CreateGroupInput input)
		{
			if (input.IsSubGroup && !string.IsNullOrEmpty(input.ParentGroupId))
			{
				var foundGroup = await groupRepository.FindByAsync(x => x.Id == input.ParentGroupId);
				if (foundGroup is not null && foundGroup.IsSubGroup)
				{
					return new GroupPayload(new UserError("You can not create a subgroup of a subgroup.", AppErrorCodes.IsSubGroup));
				}

				var newSubgroup = new Group(input.Name, input.Description, input.IsSubGroup);
				foundGroup?.AddSubGroup(newSubgroup);
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

		/// <summary>
		///
		/// </summary>
		/// <param name="groupRepository"></param>
		/// <param name="unitOfWork">The unit-of-work.</param>
		/// <param name="input"></param>
		/// <returns></returns>
		public async Task<GroupPayload> updateGroup([Service] IBaseRepositoryAsync<Group> groupRepository,
			[Service] IUnitOfWork unitOfWork,
			UpdateGroupInput input)
		{
			var foundGroup = await groupRepository.FindByAsync(x => x.Id == input.Id);
			if (foundGroup is null)
			{
				return new GroupPayload(
					new UserError($"Error: Couldn't find group with id {input.Id}.", AppErrorCodes.NotFound));
			}

			if (!string.IsNullOrEmpty(input.Name))
			{
				foundGroup.SetName(input.Name);
			}
			if (!string.IsNullOrEmpty(input.Description))
			{
				foundGroup.SetDescription(input.Description);
			}
			await unitOfWork.SaveAsync();
			// TODO hier dann über den TopicSender an eine Subscription senden
			return new GroupPayload(foundGroup, $"Updated group with name {input.Name}");
		}
	}
}