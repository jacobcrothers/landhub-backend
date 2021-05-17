using Domains.DBModels;

using MediatR;

namespace Commands
{
    public class GetUserQuery : IRequest<User>
    {
        public string UserId { get; set; }
        public string OrgId { get; set; }

    }
}
