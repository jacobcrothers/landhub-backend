using MediatR;

namespace Commands
{
    public class ExchangeTokenCommand : IRequest<string>
    {
        public string OrgId { get; set; }
        public string UserName { get; set; }
    }
}
