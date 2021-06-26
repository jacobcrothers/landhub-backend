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
            var saleswebsites = await _saleswebsiteBaseRepository.GetAllAsync(x => x.OrganizationId == request.OrgId);
            return saleswebsites.ToList();
        }
    }
}
