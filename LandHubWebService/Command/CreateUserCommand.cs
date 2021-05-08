using MediatR;

namespace Command
{
    public class CreateUserCommand : IRequest
    {
        public string Name { get; set; }
    }
}
