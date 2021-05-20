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

        public CreateUserCommandHandler(IBaseUserManager userManager
            , IMapper mapper
            , IOrganizationManager organizationManager
            , IMappingService mappingService
            )
        {
            _usermanager = userManager;
            _mapper = mapper;
            _organizationManager = organizationManager;
            _mappingService = mappingService;
        }

        protected override async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
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

            _organizationManager.CreateOrganization(organization);
            await _mappingService.MapUserOrgRole(Const.DEFAULT_ADMIN_ROLE_ID, user.Id, organization.Id);
            var result = await _usermanager.RegisterUserAsync(user, request.Password);
            return result;
        }
    }
}
