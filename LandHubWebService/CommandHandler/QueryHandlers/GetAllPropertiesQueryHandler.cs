using AutoMapper;

using Commands.Query;

using Domains.DBModels;
using Domains.Dtos;

using MediatR;

using Services.Repository;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers.QueryHandlers
{
    public class GetAllPropertiesQueryHandler : IRequestHandler<GetAllPropertiesQuery, List<PropertyForList>>
    {

        private readonly IBaseRepository<Properties> _baseRepositoryProperties;
        private readonly IMapper _mapper;


        public GetAllPropertiesQueryHandler(IMapper mapper
             , IBaseRepository<Properties> baseRepositoryProperties
           )
        {
            _mapper = mapper;
            _baseRepositoryProperties = baseRepositoryProperties;
        }

        public async Task<List<PropertyForList>> Handle(GetAllPropertiesQuery request, CancellationToken cancellationToken)
        {
            var propertyForList = new List<PropertyForList>();
            var properties = await _baseRepositoryProperties.GetAllWithPagingAsync(x => x.OrgId == request.OrgId, request.PageNumber, request.PageSize);
            foreach (var property in properties.ToList())
            {
                var propertyForUi = _mapper.Map<Properties, PropertyForList>(property);
                propertyForUi.CampaignStatus = propertyForUi.CampaignStatus ?? "Not Assigned";
                propertyForList.Add(propertyForUi);
            }

            return propertyForList;
        }

    }
}
