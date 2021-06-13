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
    public class GetAllListingQueryHandler : IRequestHandler<GetAllListingQuery, List<Listing>>
    {
        private readonly IBaseRepository<Listing> _listingBaseRepository;
        public GetAllListingQueryHandler(IBaseRepository<Listing> _listingBaseRepository)
        {
            this._listingBaseRepository = _listingBaseRepository;
        }
        public async Task<List<Listing>> Handle(GetAllListingQuery request, CancellationToken cancellationToken)
        {
            var listings = await _listingBaseRepository.GetAllAsync(x => x.OrganizationId == request.OrgId);
            return listings.ToList();
        }
    }
}
