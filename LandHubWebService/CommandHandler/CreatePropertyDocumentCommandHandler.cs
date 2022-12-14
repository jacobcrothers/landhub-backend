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
    public class CreatePropertyDocumentCommandHandler : AsyncRequestHandler<CreatePropertyDocumentCommand>
    {
        private readonly IMapper _mapper;
        private IBaseRepository<PropertyDocument> _baseRepositoryCampaign;

        public CreatePropertyDocumentCommandHandler(IMapper mapper
            , IBaseRepository<PropertyDocument> baseRepositoryCampaign
        )
        {
            _mapper = mapper;
            _baseRepositoryCampaign = baseRepositoryCampaign;
        }

        protected override async Task Handle(CreatePropertyDocumentCommand request, CancellationToken cancellationToken)
        {
            foreach(var entry in request.List)
            {
                var pd = _mapper.Map<CreatePropertyDocument, PropertyDocument>(entry);
                await _baseRepositoryCampaign.Create(pd);
            }
            
        }

    }
}
