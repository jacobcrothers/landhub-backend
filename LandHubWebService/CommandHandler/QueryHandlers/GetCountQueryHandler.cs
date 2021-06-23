using AutoMapper;

using Commands.Query;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers.QueryHandlers
{
    public class GetCountQueryHandler : IRequestHandler<GetCountQuery, long>
    {

        private readonly IBaseRepository<Team> _baseRepositoryTeam;
        private readonly IBaseRepository<User> _baseRepositoryUser;
        private readonly IBaseRepository<TeamUserMapping> _baseRepositoryTeamUserMapping;
        private readonly IMapper _mapper;


        public GetCountQueryHandler(IMapper mapper
             , IBaseRepository<Team> baseRepositoryTeam
             , IBaseRepository<TeamUserMapping> baseRepositoryTeamUserMapping
             , IBaseRepository<User> baseRepositoryUser
           )
        {
            _mapper = mapper;
            _baseRepositoryTeamUserMapping = baseRepositoryTeamUserMapping;
            _baseRepositoryTeam = baseRepositoryTeam;
            _baseRepositoryUser = baseRepositoryUser;
        }


        public async Task<long> Handle(GetCountQuery request, CancellationToken cancellationToken)
        {
            Type t = request.EntityName;
            //  var entityName = Activator.CreateInstance(BaseEntity);
            /*var teamForList = new List<TeamForUi>();
            var teamList = await _baseRepositoryTeam.GetAllWithPagingAsync(x => x.OrganizationId == request.OrgId, request.PageNumber, request.PageSize);
            foreach (var team in teamList.ToList())
            {
                var teamForUi = _mapper.Map<Team, TeamForUi>(team);
                teamForUi.Users = new List<UserForUi>();
                var teamUsers = await _baseRepositoryTeamUserMapping.GetAllAsync(x => x.TeamId == team.Id);
                foreach (var teamUserMapping in teamUsers.ToList())
                {
                    var user = await _baseRepositoryUser.GetByIdAsync(teamUserMapping.UserId);
                    var userForUi = _mapper.Map<User, UserForUi>(user);
                    teamForUi.Users.Add(userForUi);
                }
                teamForList.Add(teamForUi);
            }*/

            await Task.Delay(1);
            return 2;
        }

    }
}
