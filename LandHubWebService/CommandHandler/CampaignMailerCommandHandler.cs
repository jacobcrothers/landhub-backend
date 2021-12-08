using Commands;
using Domains.DBModels;
using MediatR;
using PropertyHatchCoreService.Services;
using Services.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class CampaignMailerCommandHandler : IRequestHandler<CampaignMailerCommand, string>
    {
        private IBaseRepository<Campaign> _campaignRepository;
        private PostCardManiaService _postCardManiaService;
        public CampaignMailerCommandHandler(IBaseRepository<Campaign> campaignRepository, PostCardManiaService postCardManiaService)
        {
            this._campaignRepository = campaignRepository;
            this._postCardManiaService = postCardManiaService;
        }

        public async Task<string> Handle(CampaignMailerCommand request, CancellationToken cancellationToken)
        {
            var orders = (await _postCardManiaService.GetAllOrdersAsync()).Count;
            return "Campaigns: " + this._campaignRepository.GetTotalCount(x => true) + ";Orders: " + orders;
        }
    }
}
