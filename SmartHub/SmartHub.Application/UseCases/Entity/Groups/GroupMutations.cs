using HotChocolate;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using SmartHub.Application.UseCases.AppFolder.AppConfigParser;
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
		public async Task<Response<string>> CreateGroup([Service] IBaseRepositoryAsync<Group> groupRepository,
			[Service] IUnitOfWork unitOfWork, CreateGroupInput input)
		{
			if (input.IsSubGroup && !string.IsNullOrEmpty(input.ParentGroupId))
			{
				var foundGroup = await groupRepository.FindByAsync(x => x.Id == input.ParentGroupId);
				if (foundGroup is not null && foundGroup.IsSubGroup)
				{
					return Response.Fail("You can not create a subgroup of a subgroup.", "");
				}
				foundGroup?.AddSubGroup(new Group(input.Name, input.Description, input.IsSubGroup));
				return Response.Ok($"Created new SubGroup with name {input.Name} for group {foundGroup?.Name}.");
			}

			var created = await groupRepository.AddAsync(new Group(input.Name, input.Description));
			if (created)
			{
				await unitOfWork.SaveAsync();
				// TODO hier dann über den TopicSender an eine Subscription senden
				return Response.Ok($"Created new Group with name {input.Name}.");
			}
			return Response.Fail($" Couldn't create new Group with name {input.Name}", "");
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="groupRepository"></param>
		/// <param name="unitOfWork">The unit-of-work.</param>
		/// <param name="input"></param>
		/// <returns></returns>
		public async Task<Response<string>> updateGroup([Service] IBaseRepositoryAsync<Group> groupRepository,
			[Service] IUnitOfWork unitOfWork,
			UpdateGroupInput input)
		{
			var foundGroup = await groupRepository.FindByAsync(x => x.Id == input.Id);
			if (foundGroup is null)
			{
				return Response.Fail($"Error: Couldn't find group with id {input.Id}.", "");
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
			return Response.Ok($"Updated group with name {input.Name}", "");
		}
	}
}