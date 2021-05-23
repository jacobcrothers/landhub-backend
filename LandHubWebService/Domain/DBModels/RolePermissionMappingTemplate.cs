using System.Collections.Generic;

namespace Domains.DBModels
{
    public class RolePermissionMappingTemplate : BaseEntity
    {
        public string Title { get; set; }
        public List<Permission> Permissions { get; set; }
    }
}
