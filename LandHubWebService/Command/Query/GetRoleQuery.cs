
using Domains.DBModels;

using MediatR;

using System.Collections.Generic;

namespace Commands.Query
{
    public class GetRoleQuery : IRequest<List<RolePermissionMappingTemplate>>
    {
        public string OrgId { get; set; }
    }
}
