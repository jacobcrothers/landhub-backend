using Commands;
using Domains.DBModels;
using MediatR;
using Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class SetOfferPriceCommandHandler : AsyncRequestHandler<SetOfferPriceCommand>
    {
        private IBaseRepository<Properties> _propertiesRepository;
        public SetOfferPriceCommandHandler(IBaseRepository<Properties> propertiesRepository)
        {
            _propertiesRepository = propertiesRepository;
        }
        protected override async Task Handle(SetOfferPriceCommand request, CancellationToken cancellationToken)
        {
            foreach (var id in request.PropertyIds)
            {
                var property = await _propertiesRepository.GetByIdAsync(id);
                property.OfferPrice = request.OfferPrice;
                await _propertiesRepository.UpdateAsync(property);
            }

        }
    }
}
