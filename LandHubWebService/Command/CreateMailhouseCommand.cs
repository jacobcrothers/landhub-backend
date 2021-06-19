using MediatR;

using System;

using System.Text.Json.Serialization;

namespace Commands
{
    public class CreateMailhouseCommand : IRequest
    {
        public string Name { get; set; }
       
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string OrganizationId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }

        [JsonIgnore]
        public string MailhouseId { get; set; }

    }
}
