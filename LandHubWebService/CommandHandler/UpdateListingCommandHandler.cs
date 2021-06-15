using AutoMapper;

using Commands;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class UpdateListingCommandHandler : AsyncRequestHandler<UpdateListingCommand>
    {
        private readonly IMapper _mapper;
        private IBaseRepository<Listing> _baseRepositoryListing;

        public UpdateListingCommandHandler(IMapper mapper
            , IBaseRepository<Listing> _baseRepositoryListing
        )
        {
            _mapper = mapper;
            this._baseRepositoryListing = _baseRepositoryListing;
        }

        protected override async Task Handle(UpdateListingCommand request, CancellationToken cancellationToken)
        {
            var listing = _mapper.Map<UpdateListingCommand, Listing>(request);
            await _baseRepositoryListing.UpdateAsync(listing);
        }

    }
}
