using Commands;

using Domains.DBModels;

using Infrastructure;

using MediatR;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

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
        private IBaseRepository<Prycd> _baseRepositoryPropertiesPrycd;
        private readonly ILogger<InitiatetFileImportCommandHandler> _logger;
        public InitiatetFileImportCommandHandler(IBaseRepository<PropertiesFileImport> baseRepositoryPropertiesFileImport
            , IBaseRepository<AgentPro> baseRepositoryPropertiesAgentPro
            , ILogger<InitiatetFileImportCommandHandler> logger
            , IBaseRepository<Prycd> baseRepositoryPropertiesPrycd)
        {
            _baseRepositoryPropertiesFileImport = baseRepositoryPropertiesFileImport;
            _baseRepositoryPropertiesAgentPro = baseRepositoryPropertiesAgentPro;
            _logger = logger;
            _baseRepositoryPropertiesPrycd = baseRepositoryPropertiesPrycd;
        }
        public async Task<string> Handle(InitiateFileImportCommand request, CancellationToken cancellationToken)
        {
            var propertiesFileImport = await _baseRepositoryPropertiesFileImport.GetSingleAsync(x => x.Id == request.FileId);
            int successRecordCount = 0;
            int failedRecordCount = 0;
            int totalRecordCount = 0;
            int alreadyExistCount = 0;

            var fileContent = System.Text.Encoding.UTF8.GetString(propertiesFileImport.FileContent).Split(
                       new[] { "\r\n", "\r", "\n" },
                       StringSplitOptions.None
                   );

            var fileColumns = fileContent.First().Split(',');

            if (propertiesFileImport.ListProvider.ToLower() == Const.PROPERTY_LIST_PROVIDER_AGENT_PRO)
            {

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
                                                var data = (JsonConvert.DeserializeObject(record[j]))?.ToString().Trim();
                                                agentPro.GetType().GetProperty(propertyName).SetValue(agentPro, data ?? string.Empty);
                                            }
                                        }
                                    }
                                    if (await IsPropertyNotAvaliableInAgentPro(agentPro.APN))
                                    {
                                        await _baseRepositoryPropertiesAgentPro.Create(agentPro);
                                        successRecordCount++;
                                    }
                                    else
                                    {
                                        alreadyExistCount++;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(ex.Message);
                                failedRecordCount++;
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            else if (propertiesFileImport.ListProvider.ToLower() == Const.PROPERTY_LIST_PROVIDER_PRYCD)
            {
                var prycdList = new List<Prycd>();
                if (propertiesFileImport.Status == "ColumnMapped")
                {
                    try
                    {
                        for (int i = 1; i < fileContent.Length; i++)
                        {
                            try
                            {
                                var record = fileContent[i].Split(',');
                                var prycd = new Prycd()
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
                                            if (prycd.HasProperty(propertyName))
                                            {
                                                var data = record[j]?.ToString().Trim();
                                                prycd.GetType().GetProperty(propertyName).SetValue(prycd, data ?? string.Empty);
                                            }
                                        }
                                    }

                                    if (await IsPropertyNotAvaliableInPrycd(prycd.APNFormatted))
                                    {
                                        await _baseRepositoryPropertiesPrycd.Create(prycd);
                                        successRecordCount++;
                                    }
                                    else
                                    {
                                        alreadyExistCount++;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(ex.Message);
                                failedRecordCount++;
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            propertiesFileImport.Message = $"Total record: {totalRecordCount}. Success import: {successRecordCount}. Existed properties: {alreadyExistCount}. Filed to import: {failedRecordCount}";
            await _baseRepositoryPropertiesFileImport.UpdateAsync(propertiesFileImport);
            return propertiesFileImport.Message;
        }

        private async Task<bool> IsPropertyNotAvaliableInAgentPro(string apn)
        {
            var property = await _baseRepositoryPropertiesAgentPro.GetSingleAsync(it => it.APN == apn);
            return property == null;
        }
        private async Task<bool> IsPropertyNotAvaliableInPrycd(string apn)
        {
            var property = await _baseRepositoryPropertiesPrycd.GetSingleAsync(it => it.APNFormatted == apn);
            return property == null;
        }
    }
}
