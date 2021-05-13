using Domains.DBModels;
using Services.IManagers;
using Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Managers
{
    public class OrganizationManager : IOrganizationManager
    {
        //private readonly IBaseRepository<> _userBaseRepository;
        private readonly IBaseRepository<Organization> _organizationBaseRepository;

        public OrganizationManager( IBaseRepository<Organization> organizationBaseRepository)
        {
            _organizationBaseRepository = organizationBaseRepository;
        }

        public void CreateOrganization(Organization organization)
        {
            _organizationBaseRepository.Create(organization);
        }
        public void UserRoleOrgMapsDetails(List<UserRoleMapping> userRoleMappings)
        {
            
        }
    }
}
