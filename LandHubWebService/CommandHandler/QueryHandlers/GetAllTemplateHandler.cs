using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Commands.Query;
using Domains.DBModels;
using MediatR;
using Services.Repository;

namespace CommandHandlers.QueryHandlers
{
    public class GetAllTemplateHandler : IRequestHandler<GetAllTemplateQuery, List<DocumentTemplate>>
    {
        private readonly IBaseRepository<DocumentTemplate> _templateListBaseRepository;
        public GetAllTemplateHandler(IBaseRepository<DocumentTemplate> templateListBaseRepository)
        {
            this._templateListBaseRepository = templateListBaseRepository;
        }

        public async Task<List<DocumentTemplate>> Handle(GetAllTemplateQuery request, CancellationToken cancellationToken)
        {
            var listings = await _templateListBaseRepository.GetAllAsync(x => x.Id != null);
            return listings.ToList();
        }
    }
}
