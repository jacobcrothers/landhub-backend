using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Commands;
using Domains.DBModels;
using MediatR;
using Services.Repository;

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
                data.Id = Guid.NewGuid().ToString();
                data.TemplateName = request.TemplateName;
                data.TemplateType = request.TemplateType;
                data.Status = request.Status;
                data.CreatedBy= request.CreatedBy;
                data.CreatedAt = new DateTime();

                await _baseRepositoryTemplate.Create(data);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
