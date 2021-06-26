
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
        private readonly IBaseRepository<User> _baseRepositoryUser;
        private readonly IBaseRepository<Role> _baseRepositoryRole;


        public GetCountQueryHandler(IBaseRepository<Team> baseRepositoryTeam
             , IBaseRepository<Role> baseRepositoryRole
             , IBaseRepository<User> baseRepositoryUser
           )
        {
            _baseRepositoryRole = baseRepositoryRole;
            _baseRepositoryTeam = baseRepositoryTeam;
            _baseRepositoryUser = baseRepositoryUser;
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
                    count = _baseRepositoryRole.GetTotalCount(x => x.OrganizationId == request.OrganizationId);
                    count += 3;
                    break;
                case "User":
                    count = _baseRepositoryUser.GetTotalCount(x => x.OrganizationId == request.OrganizationId);
                    break;
                default:
                    break;
            }
            return Task.FromResult(count);
        }

    }
}
