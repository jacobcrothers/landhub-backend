
using Commands.Query;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers.QueryHandlers
{
    public class GetCountQueryHandler : IRequestHandler<GetCountQuery, long>
    {

        private readonly IBaseRepository<Team> _baseRepositoryTeam;
        private readonly IBaseRepository<UserOrganizationMapping> _baseRepositoryUser;
        private readonly IBaseRepository<Role> _baseRepositoryRole;
        private readonly IBaseRepository<Invitation> _baseRepositoryInvitation;
        private readonly IBaseRepository<Properties> _baseRepositoryProperties;


        public GetCountQueryHandler(IBaseRepository<Team> baseRepositoryTeam
             , IBaseRepository<Role> baseRepositoryRole
             , IBaseRepository<UserOrganizationMapping> baseRepositoryUser
             , IBaseRepository<Invitation> baseRepositoryInvitation
            , IBaseRepository<Properties> baseRepositoryProperties
           )
        {
            _baseRepositoryRole = baseRepositoryRole;
            _baseRepositoryTeam = baseRepositoryTeam;
            _baseRepositoryUser = baseRepositoryUser;
            _baseRepositoryInvitation = baseRepositoryInvitation;
            _baseRepositoryProperties = baseRepositoryProperties;
        }


        public Task<long> Handle(GetCountQuery request, CancellationToken cancellationToken)
        {
            long count = 0;
            switch (request.EntityName.Name)
            {
                case "Team":
                    count = _baseRepositoryTeam.GetTotalCount(x => x.OrganizationId == request.OrganizationId);
                    break;
                case "Role":
                    count = _baseRepositoryRole.GetTotalCount(x => x.OrganizationId == request.OrganizationId || x.OrganizationId == null);
                    break;
                case "User":
                    count = _baseRepositoryUser.GetTotalCount(x => x.OrganizationId == request.OrganizationId);
                    break;
                case "Invitation":
                    count = _baseRepositoryInvitation.GetTotalCount(x => x.OrgId == request.OrganizationId);
                    break;
                case "Organization":
                    count = _baseRepositoryUser.GetTotalCount(x => x.UserId == request.UserId);
                    break;
                case "Properties":
                    count = _baseRepositoryUser.GetTotalCount(x => x.OrganizationId == request.OrganizationId);
                    break;
                default:
                    break;
            }
            return Task.FromResult(count);
        }

    }
}
