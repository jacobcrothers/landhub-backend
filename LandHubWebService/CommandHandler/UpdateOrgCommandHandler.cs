
using AutoMapper;

using Commands;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class UpdateOrgCommandHandler : AsyncRequestHandler<UpdateOrgCommand>
    {
        private readonly IBaseRepository<Organization> _organizationBaseRepository;
        private readonly IMapper _mapper;
        public UpdateOrgCommandHandler(IBaseRepository<Organization> organizationBaseRepository
            , IMapper mapper)
        {

            _organizationBaseRepository = organizationBaseRepository;
            _mapper = mapper;
        }
        protected override async Task Handle(UpdateOrgCommand request, CancellationToken cancellationToken)
        {
            var orgInDb = await _organizationBaseRepository.GetSingleAsync(x => x.Id == request.Id);
            var organization = _mapper.Map<UpdateOrgCommand, Organization>(request);
            organization.CreatedBy = request.UserId;
            await _organizationBaseRepository.UpdateAsync(organization);

        }
    }
}
