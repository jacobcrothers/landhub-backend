using AutoMapper;

using Commands;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class UpdateSalesWebsiteCommandHandler : AsyncRequestHandler<UpdateSalesWebsiteCommand>
    {
        private readonly IMapper _mapper;
        private IBaseRepository<SalesWebsite> _baseRepositorySalesWebsite;

        public UpdateSalesWebsiteCommandHandler(IMapper mapper
            , IBaseRepository<SalesWebsite> _baseRepositorySalesWebsite
        )
        {
            _mapper = mapper;
            this._baseRepositorySalesWebsite = _baseRepositorySalesWebsite;
        }

        protected override async Task Handle(UpdateSalesWebsiteCommand request, CancellationToken cancellationToken)
        {
            var saleswebsite = _mapper.Map<UpdateSalesWebsiteCommand, SalesWebsite>(request);
            await _baseRepositorySalesWebsite.UpdateAsync(saleswebsite);
        }

    }
}
