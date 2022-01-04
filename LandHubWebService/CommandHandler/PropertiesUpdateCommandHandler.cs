using Commands;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class PropertiesUpdateCommandHandler : IRequestHandler<PropertyUpdateCommand, bool>
    {
        private readonly IBaseRepository<Properties> _baseRepositoryProperties;
        public PropertiesUpdateCommandHandler(IBaseRepository<Properties> baseRepositoryProperties)
        {
            _baseRepositoryProperties = baseRepositoryProperties;
        }

        public async Task<bool> Handle(PropertyUpdateCommand request, CancellationToken cancellationToken)
        {
            var property = await _baseRepositoryProperties.GetByIdAsync(request.PropertiesId);

            property.TotalAssessedValue = request.totalAssessedValue;
            property.propertyDimension = request.propertyDimension;
            property.hoaRestriction = request.hoaRestriction;
            property.zoingRestriction = request.zoingRestriction;
            property.accessType = request.accessType;
            property.visualAccess = request.visualAccess;
            property.topography = request.topography;
            property.powerAvailable = request.powerAvailable;
            property.gasAvailable = request.gasAvailable;
            property.pertTest = request.pertTest;
            property.floodZone = request.floodZone;
            property.survey = request.survey;
            property.Zoning= request.zoning;
            property.utilities = request.utilities;

            await _baseRepositoryProperties.UpdateAsync(property);

            return true;
        }
    }
}
