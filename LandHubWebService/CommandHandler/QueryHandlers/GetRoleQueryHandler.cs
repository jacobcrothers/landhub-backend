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


        public GetRoleQueryHandler(IBaseRepository<Role> roleBaseRepository
            , IBaseRepository<RolePermissionMapping> rolePermissionMappingBaseRepository
            , IBaseRepository<Permission> permissionBaseRepository
            , IMappingService mappingService
            , IBaseRepository<RolePermissionMappingTemplate> rolePermissionMappingTemplateBaseRepository
           )
        {
            this._roleBaseRepository = roleBaseRepository;
            this._rolePermissionMappingBaseRepository = rolePermissionMappingBaseRepository;
            this._permissionBaseRepository = permissionBaseRepository;
            this._mappingService = mappingService;
            this._rolePermissionMappingTemplateBaseRepository = rolePermissionMappingTemplateBaseRepository;
        }

        public async Task<List<RolePermissionMappingTemplate>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {
            var roles = await _roleBaseRepository.GetAllAsync(x => x.OrganizationId == request.OrgId);
            var permissions = await _permissionBaseRepository.GetAsync();
            var defaultRolePermissionMapping = await _rolePermissionMappingTemplateBaseRepository.GetAsync();
            var defaultRolePermissionMappingList = defaultRolePermissionMapping.ToList();
            foreach (Role role in roles)
            {
                var defaultRoleTemplate = new RolePermissionMappingTemplate
                {
                    Id = role.Id,
                    Title = role.Title,
                    Category = role.Category,
                    Description = role.Description,
                    IsActive = role.IsActive,
                    IsShownInUi = role.IsShownInUi,
                    Permissions = new List<Permission>()
                };

                var rolePermissionMappingList = await _rolePermissionMappingBaseRepository.GetAllAsync(x => x.OrganizationId == request.OrgId && x.RoleId == role.Id);
                foreach (RolePermissionMapping mapping in rolePermissionMappingList)
                {
                    defaultRoleTemplate.Permissions.Add(permissions.FirstOrDefault(x => x.Id == mapping.PermissionId));
                }
                defaultRolePermissionMappingList.Add(defaultRoleTemplate);
            }
            return defaultRolePermissionMappingList;
        }

    }
}
