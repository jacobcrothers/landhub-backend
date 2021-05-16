using MediatR;

using System.Collections.Generic;

namespace Commands
{
    public class CreateRoleCommand : IRequest
    {
        public string RoleName { get; set; }
        public string OrgId { get; set; }
        public List<string> Permissions { get; set; }

        public CreateRoleCommand()
        {
            Permissions = new List<string>();
        }
    }
}
