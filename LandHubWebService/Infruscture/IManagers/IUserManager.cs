using Domains.DBModels;

using System.Collections.Generic;

namespace Services.IManagers
{
    public interface IUserManager
    {
        void CreateUser(User user);
        User GetUserByEmail(string email);
        void UpdateUserRoleOrgMaps(List<UserRoleMapping> userRoleMappings);
    }
}
