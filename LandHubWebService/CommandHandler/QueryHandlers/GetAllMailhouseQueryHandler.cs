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
    public class GetAllMailhouseQueryHandler : IRequestHandler<GetAllMailhouseQuery, List<Mailhouse>>
    {
        private readonly IBaseRepository<Mailhouse> _mailhouseBaseRepository;
        public GetAllMailhouseQueryHandler(IBaseRepository<Mailhouse> _mailhouseBaseRepository)
        {
            this._mailhouseBaseRepository = _mailhouseBaseRepository;
        }
        public async Task<List<Mailhouse>> Handle(GetAllMailhouseQuery request, CancellationToken cancellationToken)
        {
            var mailhouses = await _mailhouseBaseRepository.GetAllAsync(x => x.OrganizationId == request.OrgId);
            return mailhouses.ToList();
        }
    }
}
