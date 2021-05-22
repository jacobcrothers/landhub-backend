using Domains.DBModels;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.IManagers
{
    public interface IBaseUserManager
    {
        Task CreateUser(User user);
        User GetUserByEmail(string email);
        void UpdateUserRoleOrgMaps(List<UserRoleMapping> userRoleMappings);
        Task<bool> Login(string email, string password = "");
        Task<bool> RegisterUserAsync(ApplicationUser user, string password = "");
        Task<ApplicationUser> FindByNameAsync(string email);
        Task<List<UserRoleMapping>> FindRolesByUserIdByOrgIdAsync(string userId, string orgId);
        Task<List<RolePermissionMapping>> FindRolesPermissionMappingByUserIdByOrgIdAsync(string roleId, string orgId);
    }
}
