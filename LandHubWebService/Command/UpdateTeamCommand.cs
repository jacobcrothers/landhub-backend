using MediatR;

using System;
using System.Collections.Generic;

namespace Commands
{
    public class UpdateTeamCommand : IRequest
    {
        public string Id { get; set; }
        public string TeamName { get; set; }
        public string OrganizationId { get; set; }
        public List<string> Members { get; set; }
        public string Role { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public UpdateTeamCommand()
        {
            Members = new List<string>();
        }
    }
}
