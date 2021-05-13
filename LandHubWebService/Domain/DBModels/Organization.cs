using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domains.DBModels
{
    public class Organization: BaseEntity
    {
        public string Title { get; set; }
        public string ImageId { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string CreatedBy { get; set; }
    }
}
