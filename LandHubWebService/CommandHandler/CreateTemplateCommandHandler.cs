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
    public class CreateTemplateCommandHandler : AsyncRequestHandler<CreateTemplateCommand>
    {
        private readonly IMapper _mapper;
        private IBaseRepository<DocumentTemplate> _baseRepositoryTemplate;
        public CreateTemplateCommandHandler(IMapper mapper
            , IBaseRepository<DocumentTemplate> baseRepositoryTemplate)
        {

            _baseRepositoryTemplate = baseRepositoryTemplate;
            _mapper = mapper;
        }

        protected override async Task Handle(CreateTemplateCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var data = new DocumentTemplate();
                data.TemplateData = request.TemplateData;
                data.TemplateName = request.TemplateName;
                data.TemplateType = request.TemplateType;
                data.Status = request.Status;
                data.CreatedBy = request.CreatedBy;
                data.CreatedAt = DateTime.Now;
                data.OrgId = request.OrganizationId;

                await _baseRepositoryTemplate.Create(data);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
