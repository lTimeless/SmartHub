using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Entities;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.Entity.Groups.Mutations
{
	/// <summary>
	/// Endpoint for update group.
	/// </summary>
	[Authorize]
	[GraphQLDescription("Endpoint for update group.")]
	public class UpdateGroupMutation
	{
		/// <summary>
		///	Update a group.
		/// </summary>
		/// <param name="groupRepository"></param>
		/// <param name="unitOfWork">The unit-of-work.</param>
		/// <param name="input"></param>
		/// <returns></returns>
		public async Task<GroupPayload> UpdateGroup([Service] IBaseRepositoryAsync<Group> groupRepository,
			[Service] IUnitOfWork unitOfWork,
			UpdateGroupInput input)
		{
			var foundGroup = await groupRepository.FindByAsync(x => x.Id == input.Id);
			if (foundGroup == null)
			{
				return new GroupPayload(
					new UserError($"Error: Couldn't find group with id {input.Id}.", AppErrorCodes.NotFound));
			}

			foundGroup.Name = string.IsNullOrWhiteSpace(input.Name) ? foundGroup.Name : input.Name ;
			foundGroup.Description = input.Description ?? foundGroup.Description;

			await unitOfWork.SaveAsync();
			// TODO hier dann über den TopicSender an eine Subscription senden
			return new GroupPayload(foundGroup, $"Updated group with name {input.Name}");
		}
	}
}