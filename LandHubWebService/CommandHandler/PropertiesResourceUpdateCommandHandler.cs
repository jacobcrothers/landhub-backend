using Commands;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class PropertiesStatusUpdateCommandHandler : IRequestHandler<PropertiesStatusUpdateCommand, bool>
    {
        private readonly IBaseRepository<Properties> _baseRepositoryProperties;
        private readonly IBaseRepository<Listing> _baseRepositoryListing;
        public PropertiesStatusUpdateCommandHandler(IBaseRepository<Properties> baseRepositoryProperties, IBaseRepository<Listing> baseRepositoryListing)
        {
            _baseRepositoryProperties = baseRepositoryProperties;
            _baseRepositoryListing = baseRepositoryListing;
        }

        public async Task<bool> Handle(PropertiesStatusUpdateCommand request, CancellationToken cancellationToken)
        {
            var property = await _baseRepositoryProperties.GetByIdAsync(request.PropertiesId);

            if (property != null)
            {
                var listing = await _baseRepositoryListing.GetByIdAsync(property.ListingId);
                property.PropertyStatus = request.ResourceStatus.ToLower();
                if (request.ResourceStatus.ToLower() == "marketing")
                {
                    if (listing != null)
                    {
                        listing.IsMarketingSelected = true;
                        await _baseRepositoryListing.UpdateAsync(listing);
                    }
                }
                else
                {
                    if (listing is { IsMarketingSelected: true })
                    {
                        listing.IsMarketingSelected = false;
                        await _baseRepositoryListing.UpdateAsync(listing);
                    }
                }
                await _baseRepositoryProperties.UpdateAsync(property);
                return true;
            }
            return false;
        }
    }
}
