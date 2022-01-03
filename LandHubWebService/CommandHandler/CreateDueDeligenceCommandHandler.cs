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
            var dueDeligence = new DueDeligence()
            {
                OrgId = request.OrgId,
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
        }
    }
}
