using AutoMapper;

using Command;

using Domains.DBModels;

using MediatR;

using Services.IManagers;
using Services.Repository;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class CreateNewUserWithOrgCommandHandler : AsyncRequestHandler<CreateNewUserWithOrgCommand>
    {
        private readonly IMapper _mapper;
        private IBaseUserManager _usermanager;
        private IOrganizationManager _organizationManager;
        private IMappingService _mappingService;
        public CreateNewUserWithOrgCommandHandler(IBaseUserManager userManager,
                                                IOrganizationManager organizationManager,
                                                IMappingService mappingService,
                                                IMapper mapper)
        {
            _usermanager = userManager;
            _organizationManager = organizationManager;
            _mappingService = mappingService;
            _mapper = mapper;
        }
        protected override Task Handle(CreateNewUserWithOrgCommand request, CancellationToken cancellationToken)
        {

            var user = _mapper.Map<CreateNewUserWithOrgCommand, User>(request);

            var userId = Guid.NewGuid().ToString();
            var orgId = Guid.NewGuid().ToString();
            user.Id = userId;
            user.OrganizationId = orgId;

            var organization = new Organization
            {
                CreatedBy = userId,
                Id = orgId,
                Title = request.OrgTitle,
                Address = request.Address,
                Description = request.Description,
                ImageId = request.ImageId

            };

            _organizationManager.CreateOrganization(organization);
            _usermanager.CreateUser(user);
            foreach (var roleId in user.Roles)
            {
                _mappingService.MapUserOrgRole(roleId, user.Id, organization.Id);
            }

            return Task.CompletedTask;
        }
    }
}
