using System.Data.SqlTypes;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
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

        public GroupCreateCommand(string description, string name)
        {
            Description = description;
            Name = name;
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
            var newGroup = new Group(request.Name, request.Description);
            home.AddGroup(newGroup);
            return Response.Ok<string>($"Created new Group with name {request.Name}");
        }
    }
}