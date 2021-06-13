
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
        private readonly IBaseRepository<UserOrganizationMapping> userOrganizationMappingRepository;
        private readonly IBaseRepository<Organization> organizationRepository;

        public GetUserSpecificOrgQueryHandler(IBaseRepository<Organization> organizationRepository
            , IBaseRepository<UserOrganizationMapping> userOrganizationMappingRepository
           )
        {
            this.organizationRepository = organizationRepository;
            this.userOrganizationMappingRepository = userOrganizationMappingRepository;
        }

        public async Task<List<Organization>> Handle(GetUserSpecificOrgQuery request, CancellationToken cancellationToken)
        {
            var userOrganizationMappingList = await userOrganizationMappingRepository.GetAllAsync(x => x.UserId == request.UserId);
            List<Organization> orgLIst = new List<Organization>();

            foreach (UserOrganizationMapping userOrganization in userOrganizationMappingList)
            {
                var org = await organizationRepository.GetByIdAsync(userOrganization.OrganizationId);
                orgLIst.Add(org);
            }
            return orgLIst;
        }

    }
}
