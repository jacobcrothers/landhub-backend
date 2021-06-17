using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domains.DBModels
{
    public class Mailhouse : BaseEntity
    {
        public string Name { get; set; }
        public string OrganizationId { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}
