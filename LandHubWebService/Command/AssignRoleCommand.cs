using MediatR;

using System.Collections.Generic;

namespace Commands
{
    public class AssignRoleCommand : IRequest
    {
        public string UserId { get; set; }
        public string OrgId { get; set; }
        public List<string> RoleIds { get; set; }
    }
}
