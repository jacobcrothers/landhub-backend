using AutoMapper;

using Commands;

using Domains.DBModels;
using Domains.Dtos;

using MediatR;

using Services.Repository;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers.QueryHandlers
{
    public class GetAllUserByOrgQueryHandler : IRequestHandler<GetAllUserByOrgQuery, List<UserForUi>>
    {

        private readonly IBaseRepository<User> _userBaseRepository;
        private readonly IBaseRepository<UserRoleMapping> _userRoleMappingBaseRepository;
        private readonly IBaseRepository<UserOrganizationMapping> _userOrganizationMappingBaseRepository;
        private readonly IMapper _mapper;

        public GetAllUserByOrgQueryHandler(IBaseRepository<User> userBaseRepository
            , IBaseRepository<UserRoleMapping> userRoleMappingBaseRepository
            , IMapper mapper)
        {
            _userBaseRepository = userBaseRepository;
            _userRoleMappingBaseRepository = userRoleMappingBaseRepository;
            _mapper = mapper;
        }

        public async Task<List<UserForUi>> Handle(GetAllUserByOrgQuery request, CancellationToken cancellationToken)
        {
            List<UserForUi> userForUis = new List<UserForUi>();
            var userOrgList = await _userOrganizationMappingBaseRepository.GetAllAsync(x => x.OrganizationId == request.OrgId);

            foreach (UserOrganizationMapping userOrganizationMapping in userOrgList)
            {
                var user = await _userBaseRepository.GetByIdAsync(userOrganizationMapping.UserId);
                var rolesMapping = await _userRoleMappingBaseRepository.GetAllAsync(x => x.UserId == user.Id && x.OrganizationId == request.OrgId);

                List<string> rolesId = new List<string>();
                foreach (UserRoleMapping rolePermissionMapping in rolesMapping)
                {
                    rolesId.Add(rolePermissionMapping.Id);
                }
                user.Roles = rolesId;
                var uiUser = _mapper.Map<User, UserForUi>(user);
                userForUis.Add(uiUser);
            }

            return userForUis;
        }

    }
}
