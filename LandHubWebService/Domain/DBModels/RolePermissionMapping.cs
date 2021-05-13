using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domains.DBModels
{
    public class RolePermissionMapping : BaseEntity
    {
        public string RoleId { get; set; }
        public string PermissionId { get; set; }
        public string OrganizationId { get; set; }
    }
}
