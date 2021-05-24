using AutoMapper;

using Commands;

using Domains.DBModels;

using MediatR;

using PropertyHatchCoreService.IManagers;

using Services.Repository;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class CreateRoleCommandHandler : AsyncRequestHandler<CreateRoleCommand>
    {
        private readonly IMapper _mapper;

        private IRoleManager _roleManager;
        private IMappingService _mappingService;
        public CreateRoleCommandHandler(IRoleManager roleManager,
                                                IMappingService mappingService,
                                                IMapper mapper)
        {

            _roleManager = roleManager;
            _mappingService = mappingService;
            _mapper = mapper;
        }

        protected override async Task Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {

            var role = _mapper.Map<CreateRoleCommand, Role>(request);
            var roleId = Guid.NewGuid().ToString();
            role.Id = roleId;
            role.OrganizationId = request.OrgId;
            role.IsActive = true;
            role.IsShownInUi = true;
            role.Title = request.RoleName;

            await _roleManager.CreateRole(role);

            foreach (string permissionId in request.Permissions)
            {
                var rolePermissionMapping = new RolePermissionMapping()
                {
                    Id = Guid.NewGuid().ToString(),
                    OrganizationId = request.OrgId,
                    PermissionId = permissionId,
                    RoleId = roleId
                };

                await _roleManager.CreateRolePermissionMapping(rolePermissionMapping);
            }

            return Task.CompletedTask;
        }
    }
}
