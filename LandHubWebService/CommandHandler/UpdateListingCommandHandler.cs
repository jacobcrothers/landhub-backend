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
            listing.Images = new System.Collections.Generic.List<PropertyImage>()
            {
                new PropertyImage(){ Image = "http://property.myadvtcorner.com/images/pic1.jpg" , IsPrimary = true},
                new PropertyImage(){ Image = "https://images.pexels.com/photos/106399/pexels-photo-106399.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500" , IsPrimary = false}
            };
            await _baseRepositoryListing.UpdateAsync(listing);
        }

    }
}
