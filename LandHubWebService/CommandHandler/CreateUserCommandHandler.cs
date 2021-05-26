using AutoMapper;

using Commands;

using Domains.DBModels;

using Infruscture;

using MediatR;

using Services.IManagers;
using Services.Repository;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandler
{
    public class CreateUserCommandHandler : AsyncRequestHandler<CreateUserCommand>
    {
        private IBaseUserManager _usermanager;
        private readonly IMapper _mapper;
        private IOrganizationManager _organizationManager;
        private IMappingService _mappingService;
        private IBaseRepository<Invitation> _baseRepositoryInvitation;

        public CreateUserCommandHandler(IBaseUserManager userManager
            , IMapper mapper
            , IOrganizationManager organizationManager
            , IMappingService mappingService
            , IBaseRepository<Invitation> _baseRepositoryInvitation
            )
        {
            _usermanager = userManager;
            _mapper = mapper;
            _organizationManager = organizationManager;
            _mappingService = mappingService;
            this._baseRepositoryInvitation = _baseRepositoryInvitation;
        }

        protected override async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            bool result = false;
            var invitation = await _baseRepositoryInvitation.GetSingleAsync(x => x.InvitedUserEmail == request.Email);

            if (invitation == null)
            {
                var user = _mapper.Map<CreateUserCommand, ApplicationUser>(request);
                var userId = Guid.NewGuid().ToString();
                var orgId = Guid.NewGuid().ToString();
                user.Id = userId;
                user.OrganizationId = orgId;

                var organization = new Organization
                {
                    CreatedBy = userId,
                    Id = orgId,
                    Title = request.OrganizationTitle,
                    Address = request.Address
                };

                result = await _usermanager.RegisterUserAsync(user, request.Password);

                if (result)
                {
                    await _organizationManager.CreateOrganizationAsync(organization);
                    await _mappingService.MapUserOrgRole(Const.DEFAULT_ADMIN_ROLE_ID, user.Id, organization.Id);

                    var rolePermissionMappingTemplate = await _mappingService.GetRolePermissionMappingTemplateById(Const.DEFAULT_ADMIN_ROLE_ID);
                    foreach (Permission permission in rolePermissionMappingTemplate.Permissions)
                    {
                        await _mappingService.MapRolePermissionByOrg(Const.DEFAULT_ADMIN_ROLE_ID, permission, orgId);
                    }
                    await _mappingService.MapOrgUser(userId, orgId);
                }
            }
            else
            {

            }

            return result;
        }
    }
}
