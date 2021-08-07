using Commands;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class PropertiesResourceUpdateCommandHandler : IRequestHandler<PropertiesResourceUpdateCommand, bool>
    {
        private readonly IBaseRepository<Properties> _baseRepositoryProperties;
        public PropertiesResourceUpdateCommandHandler(IBaseRepository<Properties> baseRepositoryProperties)
        {
            _baseRepositoryProperties = baseRepositoryProperties;
        }

        public async Task<bool> Handle(PropertiesResourceUpdateCommand request, CancellationToken cancellationToken)
        {
            var property = await _baseRepositoryProperties.GetByIdAsync(request.PropertiesId);

            if (property != null)
            {
                if (request.ResourceType.ToLower() == "images")
                {
                    property.Images = request.Keys;
                }
                else if (request.ResourceType.ToLower() == "documents")
                {
                    property.Documents = request.Keys;
                }
                else if (request.ResourceType.ToLower() == "listing")
                {
                    property.ListingId = request.Keys[0];
                }

                await _baseRepositoryProperties.UpdateAsync(property);
                return true;
            }
            return false;
        }
    }
}
