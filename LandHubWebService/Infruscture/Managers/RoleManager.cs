using Domains.DBModels;

using PropertyHatchCoreService.IManagers;

using Services.Repository;

namespace PropertyHatchCoreService.Managers
{
    public class RoleManager : IRoleManager
    {
        private readonly IBaseRepository<Role> _roleBaseRepository;
        private readonly IBaseRepository<RolePermissionMapping> _rolePermissionMappingBaseRepository;

        public RoleManager(IBaseRepository<Role> roleBaseRepository, IBaseRepository<RolePermissionMapping> rolePermissionMappingBaseRepository)
        {
            this._roleBaseRepository = roleBaseRepository;
            this._rolePermissionMappingBaseRepository = rolePermissionMappingBaseRepository;
        }


        public void CreateRole(Role role)
        {
            _roleBaseRepository.Create(role);
        }

        public void CreateRolePermissionMapping(RolePermissionMapping rolePermissionMapping)
        {
            _rolePermissionMappingBaseRepository.Create(rolePermissionMapping);
        }
    }
}
