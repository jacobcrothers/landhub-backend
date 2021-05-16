
using Domains.DBModels;

using Services.IManagers;
using Services.Repository;

using System.Collections.Generic;

namespace Services.Managers
{
    public class OrganizationManager : IOrganizationManager
    {
        //private readonly IBaseRepository<> _userBaseRepository;
        private readonly IBaseRepository<Organization> _organizationBaseRepository;

        public OrganizationManager(IBaseRepository<Organization> organizationBaseRepository)
        {
            _organizationBaseRepository = organizationBaseRepository;
        }

        public void CreateOrganization(Organization organization)
        {
            _organizationBaseRepository.Create(organization);
        }

        public void UserRoleOrgMapsDetails(List<UserRoleMapping> userRoleMappings)
        {
            throw new System.NotImplementedException();
        }
    }
}
