using Domains.DBModels;

using MediatR;

using System.Collections.Generic;

namespace Commands.Query
{
    public class GetAllSalesWebsiteQuery : IRequest<List<SalesWebsite>>
    {
        public string OrgId { get; set; }
    }
}
