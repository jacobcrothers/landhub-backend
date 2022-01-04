using Domains.Dtos;

using MediatR;

namespace Commands.Query
{
    public class GetDueDeligenceQuery : IRequest<DueDeligenceForUi>
    {
        public string OrgId { get; set; }
        public string apn { get; set; }
        
    }
}
