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
            var pd = _mapper.Map<CreatePropertyDocumentCommand, PropertyDocument>(request);
            pd.Id = Guid.NewGuid().ToString();
            await _baseRepositoryCampaign.Create(pd);
        }

    }
}
