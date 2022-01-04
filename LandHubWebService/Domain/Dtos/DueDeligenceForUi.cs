using System;
using System.Collections.Generic;

namespace Domains.Dtos
{
    public class DueDeligenceForUi
    {
        public string Id { get; set; }
        public string OrgId { get; set; }
        public string apn { get; set; }
        public string marketValue { get; set; }
        public string propertyDimension { get; set; }
        public string hoaRestriction { get; set; }
        public string zoingRestriction { get; set; }
        public string accessType { get; set; }
        public string visualAccess { get; set; }
        public string topography { get; set; }
        public string powerAvailable { get; set; }
        public string gasAvailable { get; set; }
        public string pertTest { get; set; }
        public string floodZone { get; set; }
        public string survey { get; set; }
        public string zoing { get; set; }
        public List<string> utilities { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
