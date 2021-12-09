using System;

namespace Domains.DBModels
{
    public class Expenditure : BaseEntity
    {
        public string OrgId { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Amount { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
