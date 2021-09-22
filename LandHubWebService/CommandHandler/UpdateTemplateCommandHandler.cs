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
    public class UpdateTemplateCommandHandler : AsyncRequestHandler<UpdateTemplateCommand>
    {
        private readonly IMapper _mapper;
        private IBaseRepository<DocumentTemplate> _baseRepositoryTemplate;
        public UpdateTemplateCommandHandler(IMapper mapper
            , IBaseRepository<DocumentTemplate> baseRepositoryTemplate)
        {
            _baseRepositoryTemplate = baseRepositoryTemplate;
            _mapper = mapper;
        }

        protected override async Task Handle(UpdateTemplateCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var data = await _baseRepositoryTemplate.GetByIdAsync(request.TemplateId);
                data.TemplateData = request.TemplateData;
                data.TemplateName = request.TemplateName;
                data.TemplateType = request.TemplateType;
                data.Status = request.Status;

                await _baseRepositoryTemplate.UpdateAsync(data);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
