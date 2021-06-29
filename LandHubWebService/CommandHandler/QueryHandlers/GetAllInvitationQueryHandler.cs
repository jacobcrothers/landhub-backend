
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
        private readonly IBaseRepository<Team> _baseRepositoryTeam;


        public GetAllInvitationQueryHandler(IBaseRepository<Invitation> baseRepositoryInvitation
            , IBaseRepository<Team> baseRepositoryTeam)
        {
            _baseRepositoryInvitation = baseRepositoryInvitation;
            _baseRepositoryTeam = baseRepositoryTeam;
        }

        public async Task<List<Invitation>> Handle(GetAllInvitationQuery request, CancellationToken cancellationToken)
        {
            var teamForList = await _baseRepositoryInvitation.GetAllWithPagingAsync(x => x.OrgId == request.OrgId,
                request.PageNumber, request.PageSize);

            foreach (Invitation invitation in teamForList)
            {
                var team = await _baseRepositoryTeam.GetByIdAsync(invitation.TeamId);
                if (team != null)
                {
                    invitation.TeamId = team.TeamName;
                }
            }
            return teamForList.ToList();
        }

    }
}
