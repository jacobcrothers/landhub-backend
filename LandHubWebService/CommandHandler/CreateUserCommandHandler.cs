using Command;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace CommandHandler
{
    public class CreateUserCommandHandler : AsyncRequestHandler<CreateUserCommand>
    {
        protected override Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
