using AutoMapper;
using Commands;
using Domains.DBModels;
using MediatR;
using Services.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class UpdateCampaignCommandHandler : AsyncRequestHandler<UpdateCampaignCommand>
    {
        private readonly IMapper _mapper;
        private IBaseRepository<Campaign> _baseRepositoryCampaign;

        public UpdateCampaignCommandHandler(IMapper mapper
            , IBaseRepository<Campaign> _baseRepositoryCampaign
        )
        {
            _mapper = mapper;
            this._baseRepositoryCampaign = _baseRepositoryCampaign;
        }

        protected override async Task Handle(UpdateCampaignCommand request, CancellationToken cancellationToken)
        {
            var campaign = _mapper.Map<UpdateCampaignCommand, Campaign>(request);
            await _baseRepositoryCampaign.UpdateAsync(campaign);
        }

    }
}
