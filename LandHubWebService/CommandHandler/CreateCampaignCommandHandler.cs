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
    public class CreateCampaignCommandHandler : AsyncRequestHandler<CreateCampaignCommand>
    {
        private readonly IMapper _mapper;
        private IBaseRepository<Campaign> _baseRepositoryCampaign;

        public CreateCampaignCommandHandler(IMapper mapper
            , IBaseRepository<Campaign> baseRepositoryCampaign
        )
        {
            _mapper = mapper;
            _baseRepositoryCampaign = baseRepositoryCampaign;
        }

        protected override async Task Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
        {
            var campaign = _mapper.Map<CreateCampaignCommand, Campaign>(request);
            campaign.Id = Guid.NewGuid().ToString();
            await _baseRepositoryCampaign.Create(campaign);
        }

    }
}
