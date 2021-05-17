using Domains.DBModels;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.IManagers
{
    public interface IOrganizationManager
    {
        void UserRoleOrgMapsDetails(List<UserRoleMapping> userRoleMappings);

        void CreateOrganization(Organization organization);
        Task<Organization> GetOrganizationByCreatorAsync(string createdBy);

    }

}
