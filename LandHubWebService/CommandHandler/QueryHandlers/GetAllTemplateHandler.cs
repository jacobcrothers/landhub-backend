using Commands.Query;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers.QueryHandlers
{
    public class GetAllTemplateHandler : IRequestHandler<GetAllTemplateQuery, List<DocumentTemplate>>
    {
        private readonly IBaseRepository<DocumentTemplate> _documentListBaseRepository;
        private readonly IBaseRepository<User> _userListBaseRepository;
        public GetAllTemplateHandler(IBaseRepository<DocumentTemplate> templateListBaseRepository
            , IBaseRepository<User> userListBaseRepository)
        {
            this._documentListBaseRepository = templateListBaseRepository;
            _userListBaseRepository = userListBaseRepository;
        }

        public async Task<List<DocumentTemplate>> Handle(GetAllTemplateQuery request, CancellationToken cancellationToken)
        {
            var documentForList = new List<DocumentTemplate>();
            if (request.SearchKey == null || request.SearchKey == "")
            {
                var documents = await _documentListBaseRepository.GetAllWithPagingAsync(x => x.OrgId == request.OrganizationId, request.PageNumber, request.PageSize);

                foreach (DocumentTemplate template in documents)
                {
                    var user = await _userListBaseRepository.GetByIdAsync(template.CreatedBy);
                    template.CreatedBy = user.DisplayName;
                }
                return documents.ToList();
            } else
            {
                var documents = await _documentListBaseRepository.GetAllWithPagingAsync(x => x.OrgId == request.OrganizationId, request.PageNumber, request.PageSize);

                foreach (DocumentTemplate template in documents)
                {
                    if (template.TemplateName.Contains(request.SearchKey))
                    {
                        var user = await _userListBaseRepository.GetByIdAsync(template.CreatedBy);
                        template.CreatedBy = user.DisplayName;
                        documentForList.Add(template);
                    }
                }
                return documentForList;
            }
        }
    }
}
