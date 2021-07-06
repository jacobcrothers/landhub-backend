using Commands;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class InitiatetFileImportCommandHandler : IRequestHandler<InitiateFileImportCommand, string>
    {
        private IBaseRepository<PropertiesFileImport> _baseRepositoryPropertiesFileImport;
        public InitiatetFileImportCommandHandler(IBaseRepository<PropertiesFileImport> baseRepositoryPropertiesFileImport)
        {
            _baseRepositoryPropertiesFileImport = baseRepositoryPropertiesFileImport;
        }
        public async Task<string> Handle(InitiateFileImportCommand request, CancellationToken cancellationToken)
        {
            var propertiesFileImport = await _baseRepositoryPropertiesFileImport.GetSingleAsync(x => x.Id == request.FileId);
            if (propertiesFileImport.Status == "ColumnMapped")
            {

            }

            return "";
        }
    }
}
