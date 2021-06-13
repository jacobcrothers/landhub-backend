using Domains.DBModels;

using MediatR;

using System.Collections.Generic;

namespace Commands.Query
{
    public class GetAllListingQuery : IRequest<List<Listing>>
    {
        public string OrgId { get; set; }
    }
}
