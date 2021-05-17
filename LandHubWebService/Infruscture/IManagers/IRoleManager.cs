using Domains.DBModels;

using System.Collections.Generic;

namespace PropertyHatchCoreService.IManagers
{
    public interface IRoleManager
    {
        void CreateRole(Role role);
        void CreateRolePermissionMapping(RolePermissionMapping rolePermissionMapping);
        List<Role> GetRoleByUserByOrgAsync(string userId, string orgId);
    }
}
