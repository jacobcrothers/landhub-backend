using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domains.DBModels
{
    public class Role : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool IsActive { get; set; }
        public bool IsShownInUi { get; set; }
    }
}
