using Domains;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Repository
{
    public interface IMappingService 
    {
        public Task MapUserOrgRole(string roleId, string userId, string organizationId);

    }
}
