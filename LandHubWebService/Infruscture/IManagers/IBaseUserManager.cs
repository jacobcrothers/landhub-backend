using Domains.DBModels;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.IManagers
{
    public interface IBaseUserManager
    {
        void CreateUser(User user);
        User GetUserByEmail(string email);
        void UpdateUserRoleOrgMaps(List<UserRoleMapping> userRoleMappings);
        Task<bool> Login(string email, string password = "");
        Task<bool> RegisterUserAsync(ApplicationUser user, string password = "");
        Task<ApplicationUser> FindByNameAsync(string email);
        Task<List<UserRoleMapping>> FindRolesByUserIdByOrgIdAsync(string userId, string orgId);
    }
}
