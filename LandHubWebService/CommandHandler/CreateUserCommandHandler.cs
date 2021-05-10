using AutoMapper;

using Command;

using Domains.DBModels;

using MediatR;

using Services.IManagers;

using System.Threading;
using System.Threading.Tasks;

namespace CommandHandler
{
    public class CreateUserCommandHandler : AsyncRequestHandler<CreateUserCommand>
    {
        private IUserManager _usermanager;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUserManager userManager,
                                          IMapper mapper
            )
        {
            _usermanager = userManager;
            _mapper = mapper;

        }

        protected override Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            var user = _mapper.Map<CreateUserCommand, User>(request);
            _usermanager.CreateUser(user);
            return Task.CompletedTask;
        }
    }
}
