using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.UseCases.Entity.Groups
{
	public class GroupCreateCommand : IRequest<Response<string>>
    {
        public string Name { get; }
        public string Description { get; }
        public bool IsSubGroup { get; }
        public string? ParentGroupId { get; }

        public GroupCreateCommand(string description, string name, string? parentGroupId, bool isSubGroup = default)
        {
            Description = description;
            Name = name;
            IsSubGroup = isSubGroup;
            ParentGroupId = parentGroupId;
        }
    }

    public class GroupCreateHandler : IRequestHandler<GroupCreateCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GroupCreateHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<string>> Handle(GroupCreateCommand request, CancellationToken cancellationToken)
        {
            var home = await _unitOfWork.HomeRepository.GetHome();
            if (request.IsSubGroup && !string.IsNullOrEmpty(request.ParentGroupId))
            {
                var foundGroup = home?.Groups.Find(x => x.Id == request.ParentGroupId);
                if (foundGroup is not null && foundGroup.IsSubGroup)
                {
                    return Response.Ok("You can not create a subgroup of a subgroup.");
                }
                foundGroup?.AddSubGroup(new Group(request.Name, request.Description, request.IsSubGroup));
                return Response.Ok<string>($"Created new SubGroup with name {request.Name} for group {foundGroup?.Name}.");
            }
            home?.AddGroup(new Group(request.Name, request.Description));
            return Response.Ok<string>($"Created new Group with name {request.Name}.");
        }
    }
}