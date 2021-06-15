using MediatR;

using System.Text.Json.Serialization;

namespace Commands
{
    public class UpdateOrgCommand : IRequest
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ImageId { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        [JsonIgnore]
        public string UserId { get; set; }

    }
}
