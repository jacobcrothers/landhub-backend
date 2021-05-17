using Commands;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers.QueryHandlers
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, User>
    {

        private readonly IBaseRepository<User> _userBaseRepository;
        private readonly IBaseRepository<UserRoleMapping> _userRoleMappingBaseRepository;

        public GetUserQueryHandler(IBaseRepository<User> userBaseRepository
            , IBaseRepository<UserRoleMapping> userRoleMappingBaseRepository)
        {
            _userBaseRepository = userBaseRepository;
            _userRoleMappingBaseRepository = userRoleMappingBaseRepository;
        }

        public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userBaseRepository.Get(request.UserId);
            var rolesMapping = await _userRoleMappingBaseRepository.GetAllAsync(x => x.OrganizationId == request.OrgId && x.UserId == request.UserId);
            List<string> rolesId = new List<string>();
            foreach (UserRoleMapping rolePermissionMapping in rolesMapping)
            {
                rolesId.Add(rolePermissionMapping.Id);
            }
            user.Roles = rolesId;
            return user;
        }

    }
}
