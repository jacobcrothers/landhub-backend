using Domains.DBModels;

using System.Collections.Generic;

namespace Services.IManagers
{
    public interface IOrganizationManager
    {
        void UserRoleOrgMapsDetails(List<UserRoleMapping> userRoleMappings);

        void CreateOrganization(Organization organization);

    }

}
