﻿using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Commands
{
    public class CreateSalesWebsiteCommand : IRequest
    {
        public string WebsiteName { get; set; }
        public string WebAddress { get; set; }
        public string Type { get; set; }
        public string PhoneNumber { get; set; }
        public string HomeNavColor { get; set; }
        public string HomeHeaderText1 { get; set; }
        public string HomeHeaderText2 { get; set; }
        public string HomeHeaderText3 { get; set; }
        public string HomeInventoryHeading { get; set; }
        public string HomeInvenotrySub { get; set; }
        public string HomeHeaderPhoto { get; set; }
        public string HomeFooterText { get; set; }
        public string InventoryHeaderPhoto { get; set; }
        public string IboutPagePhoto { get; set; }
        public string AboutPageDescription { get; set; }
        public string ContactPageAddress { get; set; }
        public string ContactPageTel1 { get; set; }
        public string ContactPageTel2 { get; set; }
        public string ContactPageEmail { get; set; }
        public string TermsDescription { get; set; }
        public string OrganizationId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }

        [JsonIgnore]
        public string SaleswebsiteId { get; set; }

    }
}