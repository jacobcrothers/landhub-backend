using AutoMapper;

using Commands.Query;

using Domains.DBModels;
using Domains.Dtos;

using MediatR;

using Services.Repository;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers.QueryHandlers
{
    public class GetAllUserByOrgQueryHandler : IRequestHandler<GetAllUserByOrgQuery, List<UserForUi>>
    {

        private readonly IBaseRepository<User> _userBaseRepository;
        private readonly IBaseRepository<Role> _roleBaseRepository;
        private readonly IBaseRepository<Team> _teamBaseRepository;
        private readonly IBaseRepository<TeamUserMapping> _teamUserRoleBaseRepository;

        private readonly IBaseRepository<UserRoleMapping> _userRoleMappingBaseRepository;
        private readonly IBaseRepository<UserOrganizationMapping> _userOrganizationMappingBaseRepository;
        private readonly IMapper _mapper;

        public GetAllUserByOrgQueryHandler(IBaseRepository<User> userBaseRepository
            , IBaseRepository<UserRoleMapping> userRoleMappingBaseRepository
            , IMapper mapper
            , IBaseRepository<UserOrganizationMapping> userOrganizationMappingBaseRepository
            , IBaseRepository<Role> roleBaseRepository
            , IBaseRepository<Team> teamBaseRepository
            , IBaseRepository<TeamUserMapping> teamUserRoleBaseRepository)
        {
            _userBaseRepository = userBaseRepository;
            _userRoleMappingBaseRepository = userRoleMappingBaseRepository;
            _mapper = mapper;
            _userOrganizationMappingBaseRepository = userOrganizationMappingBaseRepository;
            _roleBaseRepository = roleBaseRepository;
            _teamBaseRepository = teamBaseRepository;
            _teamUserRoleBaseRepository = teamUserRoleBaseRepository;
        }

        public async Task<List<UserForUi>> Handle(GetAllUserByOrgQuery request, CancellationToken cancellationToken)
        {
            List<UserForUi> userForUis = new List<UserForUi>();
            var userOrgList = await _userOrganizationMappingBaseRepository.GetAllWithPagingAsync(x => x.OrganizationId == request.OrgId, request.PageNumber, request.PageSize);

            foreach (UserOrganizationMapping userOrganizationMapping in userOrgList)
            {
                var user = await _userBaseRepository.GetByIdAsync(userOrganizationMapping.UserId);
                var rolesMapping = await _userRoleMappingBaseRepository.GetAllAsync(x => x.UserId == user.Id && x.OrganizationId == request.OrgId);

                List<string> rolesId = new List<string>();
                foreach (UserRoleMapping rolePermissionMapping in rolesMapping)
                {
                    rolesId.Add(rolePermissionMapping.RoleId);
                }
                user.Roles = rolesId;
                var uiUser = _mapper.Map<User, UserForUi>(user);
                if (rolesId.Count > 0)
                {
                    var role = await _roleBaseRepository.GetByIdAsync(rolesId.First());
                    uiUser.RoleName = (role == null) ? "N/A" : role.Title;
                }
                else
                {
                    uiUser.RoleName = "N/A";
                }

                var teamUsersMapping = await _teamUserRoleBaseRepository.GetAllAsync(x =>
                    x.OrganizationId == request.OrgId && x.UserId == userOrganizationMapping.UserId);
                if (teamUsersMapping != null)
                {
                    var team = await _teamBaseRepository.GetSingleAsync(x => x.Id == teamUsersMapping.FirstOrDefault().TeamId);
                    if (team != null)
                    {
                        uiUser.TeamName = team.TeamName;
                    }
                }
                else
                {
                    uiUser.TeamName = "N/A";
                }

                uiUser.Status = user.Status ?? "Active";

                userForUis.Add(uiUser);
            }

            return userForUis;
        }

    }
}
