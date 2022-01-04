using AutoMapper;
using Commands.Query;
using Domains.DBModels;

using MediatR;
using Domains.Dtos;

using Services.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers.QueryHandlers
{
    public class GetDueDeligenceQueryHandler : IRequestHandler<GetDueDeligenceQuery, DueDeligenceForUi>
    {
        private readonly IBaseRepository<DueDeligence> _dueDeligenceBaseRepository;
        public GetDueDeligenceQueryHandler(IBaseRepository<DueDeligence> dueDeligenceBaseRepository)
        {
            _dueDeligenceBaseRepository = dueDeligenceBaseRepository;
        }

        public async Task<DueDeligenceForUi> Handle(GetDueDeligenceQuery request, CancellationToken cancellationToken)
        {
            var dueDeligence = await _dueDeligenceBaseRepository.GetSingleAsync(x => x.apn == request.apn);
            if (dueDeligence != null)
            {
                var dueDeligenceForUi = new DueDeligenceForUi
                {
                    Id = dueDeligence.Id,
                    OrgId = dueDeligence.OrgId,
                    apn = dueDeligence.apn,
                    marketValue = dueDeligence.marketValue,
                    propertyDimension = dueDeligence.propertyDimension,
                    hoaRestriction = dueDeligence.hoaRestriction,
                    zoingRestriction = dueDeligence.zoingRestriction,
                    accessType = dueDeligence.accessType,
                    visualAccess = dueDeligence.visualAccess,
                    topography = dueDeligence.topography,
                    powerAvailable = dueDeligence.powerAvailable,
                    gasAvailable = dueDeligence.gasAvailable,
                    pertTest = dueDeligence.pertTest,
                    floodZone = dueDeligence.floodZone,
                    survey = dueDeligence.survey,
                    zoing = dueDeligence.zoing,
                    utilities = dueDeligence.utilities,
                    CreatedDate = dueDeligence.CreatedDate
                };

                return dueDeligenceForUi;
            } else
            {
                return null;
            }
        }

    }
}
