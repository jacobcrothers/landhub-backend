using Domains.DBModels;

namespace PropertyHatchCoreService.IManagers
{
    public interface IRoleManager
    {
        void CreateRole(Role role);
        void CreateRolePermissionMapping(RolePermissionMapping rolePermissionMapping);
    }
}
