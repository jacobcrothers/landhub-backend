using Domains.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IManagers
{
    public interface IOrganizationManager
    {
        public void UserRoleOrgMapsDetails(List<UserRoleMapping> userRoleMappings);

        void CreateOrganization(Organization organization);
       
    }

}
