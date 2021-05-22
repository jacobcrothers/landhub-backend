﻿using System.Threading.Tasks;

namespace Services.Repository
{
    public interface IMappingService
    {
        Task MapUserOrgRole(string roleId, string userId, string organizationId);

    }
}
