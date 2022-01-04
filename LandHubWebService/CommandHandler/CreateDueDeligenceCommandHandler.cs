using AutoMapper;

using Commands;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class CreateDueDeligenceCommandHandler : AsyncRequestHandler<CreateDueDeligenceCommand>
    {
        private readonly IMapper _mapper;
        private IBaseRepository<DueDeligence> _baseRepositoryDueDeligence;

        public CreateDueDeligenceCommandHandler(IMapper mapper, IBaseRepository<DueDeligence> baseRepositoryDueDeligence)
        {
            _mapper = mapper;
            _baseRepositoryDueDeligence = baseRepositoryDueDeligence;
        }

        protected override async Task Handle(CreateDueDeligenceCommand request, CancellationToken cancellationToken)
        {
            var dueDeligenceExist = await _baseRepositoryDueDeligence.GetSingleAsync(x => x.apn == request.apn);

            if (dueDeligenceExist == null)
            {
                var dueDeligence = new DueDeligence()
                {
                    OrgId = request.OrgId,
                    apn = request.apn,
                    marketValue = request.marketValue,
                    propertyDimension = request.propertyDimension,
                    hoaRestriction = request.hoaRestriction,
                    zoingRestriction = request.zoingRestriction,
                    accessType = request.accessType,
                    visualAccess = request.visualAccess,
                    topography = request.topography,
                    powerAvailable = request.powerAvailable,
                    gasAvailable = request.gasAvailable,
                    pertTest = request.pertTest,
                    floodZone = request.floodZone,
                    survey = request.survey,
                    zoing = request.zoing,
                    utilities = request.utilities,
                    CreatedDate = request.CreatedDate
                };
                dueDeligence.Id = Guid.NewGuid().ToString();
                await _baseRepositoryDueDeligence.Create(dueDeligence);
            } else
            {
                dueDeligenceExist.marketValue = request.marketValue;
                dueDeligenceExist.propertyDimension = request.propertyDimension;
                dueDeligenceExist.hoaRestriction = request.hoaRestriction;
                dueDeligenceExist.zoingRestriction = request.zoingRestriction;
                dueDeligenceExist.accessType = request.accessType;
                dueDeligenceExist.visualAccess = request.visualAccess;
                dueDeligenceExist.topography = request.topography;
                dueDeligenceExist.powerAvailable = request.powerAvailable;
                dueDeligenceExist.gasAvailable = request.gasAvailable;
                dueDeligenceExist.pertTest = request.pertTest;
                dueDeligenceExist.floodZone = request.floodZone;
                dueDeligenceExist.survey = request.survey;
                dueDeligenceExist.zoing = request.zoing;
                dueDeligenceExist.utilities = request.utilities;
                await _baseRepositoryDueDeligence.UpdateAsync(dueDeligenceExist);
            }
        }
    }
}
