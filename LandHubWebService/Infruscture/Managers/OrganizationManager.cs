
using Domains.DBModels;

using Services.IManagers;
using Services.Repository;

using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<Organization> GetSingleOrganizationByCreatorAsync(string createdBy)
        {
            return await _organizationBaseRepository.GetSingleAsync(it => it.CreatedBy == createdBy);
        }

        public void UserRoleOrgMapsDetails(List<UserRoleMapping> userRoleMappings)
        {
            throw new System.NotImplementedException();
        }
    }
}
