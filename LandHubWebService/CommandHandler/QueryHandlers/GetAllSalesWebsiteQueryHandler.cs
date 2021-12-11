using Commands.Query;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers.QueryHandlers
{
    public class GetAllSalesWebsiteQueryHandler : IRequestHandler<GetAllSalesWebsiteQuery, List<SalesWebsite>>
    {
        private readonly IBaseRepository<SalesWebsite> _saleswebsiteBaseRepository;
        public GetAllSalesWebsiteQueryHandler(IBaseRepository<SalesWebsite> _saleswebsiteBaseRepository)
        {
            this._saleswebsiteBaseRepository = _saleswebsiteBaseRepository;
        }
        public async Task<List<SalesWebsite>> Handle(GetAllSalesWebsiteQuery request, CancellationToken cancellationToken)
        {
            var saleswebsitesForList = new List<SalesWebsite>();
            if (request.SearchKey == null || request.SearchKey == "") {
                var saleswebsites = await _saleswebsiteBaseRepository.GetAllWithPagingAsync(x => x.OrganizationId == request.OrgId, request.PageNumber, request.PageSize);
                return saleswebsites.ToList();
            } else
            {
                var saleswebsites = await _saleswebsiteBaseRepository.GetAllWithPagingAsync(x => x.OrganizationId == request.OrgId, request.PageNumber, request.PageSize);
                foreach (var saleswebsite in saleswebsites)
                {
                    if (saleswebsite.WebAddress.Contains(request.SearchKey))
                    {
                        saleswebsitesForList.Add(saleswebsite);
                    }
                }
                return saleswebsitesForList;
            }
        }
    }
}
