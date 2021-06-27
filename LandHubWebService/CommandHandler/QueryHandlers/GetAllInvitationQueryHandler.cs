
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
    public class GetAllInvitationQueryHandler : IRequestHandler<GetAllInvitationQuery, List<Invitation>>
    {

        private readonly IBaseRepository<Invitation> _baseRepositoryInvitation;


        public GetAllInvitationQueryHandler(IBaseRepository<Invitation> baseRepositoryInvitation)
        {
            _baseRepositoryInvitation = baseRepositoryInvitation;
        }

        public async Task<List<Invitation>> Handle(GetAllInvitationQuery request, CancellationToken cancellationToken)
        {
            var teamForList = await _baseRepositoryInvitation.GetAllWithPagingAsync(x => x.OrgId == request.OrgId,
                request.PageNumber, request.PageSize);

            return teamForList.ToList();
        }

    }
}
