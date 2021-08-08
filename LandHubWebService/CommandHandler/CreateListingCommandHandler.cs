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
    public class CreateListingCommandHandler : IRequestHandler<CreateListingCommand, string>
    {
        private readonly IMapper _mapper;
        private IBaseRepository<Listing> _baseRepositoryListing;

        public CreateListingCommandHandler(IMapper mapper
            , IBaseRepository<Listing> _baseRepositoryListing
        )
        {
            _mapper = mapper;
            this._baseRepositoryListing = _baseRepositoryListing;
        }

        public async Task<string> Handle(CreateListingCommand request, CancellationToken cancellationToken)
        {
            var listing = _mapper.Map<CreateListingCommand, Listing>(request);
            listing.Id = Guid.NewGuid().ToString();
            await _baseRepositoryListing.Create(listing);
            return listing.Id;
        }

    }
}
