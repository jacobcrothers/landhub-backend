using MediatR;

namespace Commands
{
    public class MigrateDataCommand : IRequest<string>
    {
        public string FileId { get; set; }
    }
}
