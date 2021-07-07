using Commands;

using Domains.DBModels;

using MediatR;

using PropertyHatchCoreService;

using Services.Repository;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class InitiatetFileImportCommandHandler : IRequestHandler<InitiateFileImportCommand, string>
    {
        private IBaseRepository<PropertiesFileImport> _baseRepositoryPropertiesFileImport;
        private IBaseRepository<AgentPro> _baseRepositoryPropertiesAgentPro;
        public InitiatetFileImportCommandHandler(IBaseRepository<PropertiesFileImport> baseRepositoryPropertiesFileImport
            , IBaseRepository<AgentPro> baseRepositoryPropertiesAgentPro)
        {
            _baseRepositoryPropertiesFileImport = baseRepositoryPropertiesFileImport;
            _baseRepositoryPropertiesAgentPro = baseRepositoryPropertiesAgentPro;
        }
        public async Task<string> Handle(InitiateFileImportCommand request, CancellationToken cancellationToken)
        {
            var propertiesFileImport = await _baseRepositoryPropertiesFileImport.GetSingleAsync(x => x.Id == request.FileId);
            int successRecordCount = 0;
            int failedRecordCount = 0;
            int totalRecordCount = 0;

            var fileContent = System.Text.Encoding.UTF8.GetString(propertiesFileImport.FileContent).Split(
                   new[] { "\r\n", "\r", "\n" },
                   StringSplitOptions.None
               );

            var fileColumns = fileContent.First().Split(',');

            var agentProList = new List<AgentPro>();
            if (propertiesFileImport.Status == "ColumnMapped")
            {
                try
                {
                    for (int i = 1; i < fileContent.Length; i++)
                    {
                        try
                        {
                            var record = fileContent[i].Split(',');
                            var agentPro = new AgentPro()
                            {
                                Id = Guid.NewGuid().ToString(),
                                UserId = propertiesFileImport.UserId,
                                OrgId = propertiesFileImport.OrgId,
                                ImportFileId = propertiesFileImport.Id
                            };

                            if (record.Count() > 1)
                            {
                                totalRecordCount++;
                                for (int j = 0; j < fileColumns.Length; j++)
                                {
                                    if (propertiesFileImport.ColumnMapping.ContainsValue(fileColumns[j]))
                                    {
                                        var propertyName = propertiesFileImport.ColumnMapping.FirstOrDefault(x => x.Value == fileColumns[j]).Key;
                                        if (agentPro.HasProperty(propertyName))
                                        {
                                            agentPro.GetType().GetProperty(propertyName).SetValue(agentPro, (record[j]).Trim());
                                        }
                                    }
                                }
                                await _baseRepositoryPropertiesAgentPro.Create(agentPro);
                                successRecordCount++;
                            }
                        }
                        catch (Exception ex)
                        {
                            failedRecordCount++;
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            propertiesFileImport.Message = $"Total record: {totalRecordCount}. Success import: {successRecordCount}. Filed to import: {failedRecordCount}";
            await _baseRepositoryPropertiesFileImport.UpdateAsync(propertiesFileImport);
            return propertiesFileImport.Message;
        }
    }
}
