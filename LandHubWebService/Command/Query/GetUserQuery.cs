using MediatR;

namespace Commands
{
    public class GetUserQuery : IRequest
    {
        public string UserId { get; set; }
        public string OrgId { get; set; }

    }
}
