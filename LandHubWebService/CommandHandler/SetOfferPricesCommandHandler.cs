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
    public class SetOfferPricesCommandHandler : AsyncRequestHandler<SetOfferPricesCommand>
    {
        private IBaseRepository<Properties> _propertiesRepository;
        public SetOfferPricesCommandHandler(IBaseRepository<Properties> propertiesRepository)
        {
            _propertiesRepository = propertiesRepository;
        }
        protected override async Task Handle(SetOfferPricesCommand request, CancellationToken cancellationToken)
        {
            foreach (var entry in request.SetOfferPrices)
            {
                var property = await _propertiesRepository.GetByIdAsync(entry.PropertyId);
                property.OfferPrice = entry.OfferPrice;
                await _propertiesRepository.UpdateAsync(property);
            }
        }
    }
}
