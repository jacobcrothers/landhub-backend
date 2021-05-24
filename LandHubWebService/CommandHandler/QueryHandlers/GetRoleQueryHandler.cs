using Commands.Query;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers.QueryHandlers
{
    public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, List<RolePermissionMappingTemplate>>
    {

        private readonly IBaseRepository<Role> _roleBaseRepository;
        private readonly IBaseRepository<RolePermissionMapping> _rolePermissionMappingBaseRepository;
        private readonly IBaseRepository<RolePermissionMappingTemplate> _rolePermissionMappingTemplateBaseRepository;
        private readonly IBaseRepository<Permission> _permissionBaseRepository;
        private readonly IMappingService _mappingService;


        public GetRoleQueryHandler(IBaseRepository<Role> _roleBaseRepository
            , IBaseRepository<RolePermissionMapping> _rolePermissionMappingBaseRepository
            , IBaseRepository<Permission> _permissionBaseRepository
            , IMappingService _mappingService
            , IBaseRepository<RolePermissionMappingTemplate> _rolePermissionMappingTemplateBaseRepository
           )
        {
            this._roleBaseRepository = _roleBaseRepository;
            this._rolePermissionMappingBaseRepository = _rolePermissionMappingBaseRepository;
            this._permissionBaseRepository = _permissionBaseRepository;
            this._mappingService = _mappingService;
            this._rolePermissionMappingTemplateBaseRepository = _rolePermissionMappingTemplateBaseRepository;
        }

        public async Task<List<RolePermissionMappingTemplate>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {
            var roles = await _roleBaseRepository.GetAllAsync(x => x.OrganizationId == request.OrgId);
            var permissions = await _permissionBaseRepository.GetAsync();
            var defaultRolePermissionMapping = await _rolePermissionMappingTemplateBaseRepository.GetAsync();

            foreach (Role role in roles)
            {
                var defaultRoleTemplate = new RolePermissionMappingTemplate
                {
                    Id = role.Id,
                    Title = role.Title,
                };

                var rolePermissionMappingList = await _rolePermissionMappingBaseRepository.GetAllAsync(x => x.OrganizationId == request.OrgId && x.RoleId == role.Id);
                foreach (RolePermissionMapping mapping in rolePermissionMappingList)
                {
                    defaultRoleTemplate.Permissions.Add(permissions.FirstOrDefault(x => x.Id == mapping.PermissionId));
                }
                defaultRolePermissionMapping.ToList().Add(defaultRoleTemplate);
            }
            return defaultRolePermissionMapping.ToList();
        }

    }
}
