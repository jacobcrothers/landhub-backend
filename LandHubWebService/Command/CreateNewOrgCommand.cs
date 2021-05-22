using MediatR;

namespace Commands
{
    public class CreateNewOrgCommand : IRequest
    {
        public string OrgName { get; set; }
        public string OrgTitle { get; set; }
        public string ImageId { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string UserId { get; set; }

    }
}
