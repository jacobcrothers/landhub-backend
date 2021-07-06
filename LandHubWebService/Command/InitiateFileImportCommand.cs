using MediatR;

namespace Commands
{
    public class InitiateFileImportCommand : IRequest<string>
    {
        public string FileId { get; set; }
    }
}
