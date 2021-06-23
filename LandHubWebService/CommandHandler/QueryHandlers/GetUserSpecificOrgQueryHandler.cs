
using Commands;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers.QueryHandlers
{
    public class GetUserSpecificOrgQueryHandler : IRequestHandler<GetUserSpecificOrgQuery, List<Organization>>
    {
        private readonly IBaseRepository<UserOrganizationMapping> _userOrganizationMappingRepository;
        private readonly IBaseRepository<Organization> _organizationRepository;
        private readonly IBaseRepository<User> _userRepository;

        public GetUserSpecificOrgQueryHandler(IBaseRepository<Organization> organizationRepository
            , IBaseRepository<UserOrganizationMapping> userOrganizationMappingRepository
            , IBaseRepository<User> userRepository
           )
        {
            _organizationRepository = organizationRepository;
            _userOrganizationMappingRepository = userOrganizationMappingRepository;
            _userRepository = userRepository;
        }

        public async Task<List<Organization>> Handle(GetUserSpecificOrgQuery request, CancellationToken cancellationToken)
        {
            var userOrganizationMappingList = await _userOrganizationMappingRepository.GetAllAsync(x => x.UserId == request.UserId);
            List<Organization> orgLIst = new List<Organization>();

            foreach (UserOrganizationMapping userOrganization in userOrganizationMappingList)
            {
                var org = await _organizationRepository.GetByIdAsync(userOrganization.OrganizationId);
                var user = await _userRepository.GetByIdAsync(org.CreatedBy);
                if (user != null)
                {
                    org.AdminName = user.DisplayName;
                }
                orgLIst.Add(org);
            }
            return orgLIst;
        }

    }
}
