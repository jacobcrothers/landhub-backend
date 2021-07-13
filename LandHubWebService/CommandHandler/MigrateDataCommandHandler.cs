using AutoMapper;

using Commands;

using Domains.DBModels;

using Infrastructure;

using MediatR;

using Services.Repository;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class MigrateDataCommandHandler : IRequestHandler<MigrateDataCommand, string>
    {
        private IBaseRepository<PropertiesFileImport> _baseRepositoryPropertiesFileImport;
        private IBaseRepository<AgentPro> _baseRepositoryPropertiesAgentPro;
        private IBaseRepository<Prycd> _baseRepositoryPropertiesPrycd;
        private IBaseRepository<Properties> _baseRepositoryProperties;
        private IMapper _mapper;
        public MigrateDataCommandHandler(IBaseRepository<PropertiesFileImport> baseRepositoryPropertiesFileImport
            , IBaseRepository<AgentPro> baseRepositoryPropertiesAgentPro
            , IMapper mapper
            , IBaseRepository<Properties> baseRepositoryProperties
            , IBaseRepository<Prycd> baseRepositoryPropertiesPrycd)
        {
            _baseRepositoryPropertiesFileImport = baseRepositoryPropertiesFileImport;
            _baseRepositoryPropertiesAgentPro = baseRepositoryPropertiesAgentPro;
            _mapper = mapper;
            _baseRepositoryProperties = baseRepositoryProperties;
            _baseRepositoryPropertiesPrycd = baseRepositoryPropertiesPrycd;
        }
        public async Task<string> Handle(MigrateDataCommand request, CancellationToken cancellationToken)
        {
            var propertiesFileImport = await _baseRepositoryPropertiesFileImport.GetSingleAsync(x => x.Id == request.FileId);
            int successRecordCount = 0;
            int failedRecordCount = 0;
            int totalRecordCount = 0;

            if (propertiesFileImport.ListProvider.ToLower() == Const.PROPERTY_LIST_PROVIDER_AGENT_PRO)
            {
                var agentProData = await _baseRepositoryPropertiesAgentPro.GetAllAsync(x => x.ImportFileId == request.FileId);
                foreach (AgentPro agentPro in agentProData)
                {
                    try
                    {
                        var properties = _mapper.Map<AgentPro, Properties>(agentPro);
                        await _baseRepositoryProperties.Create(properties);
                        successRecordCount++;
                    }
                    catch (Exception ex)
                    {
                        failedRecordCount++;
                    }
                    totalRecordCount++;
                }
            }
            else if (propertiesFileImport.ListProvider.ToLower() == Const.PROPERTY_LIST_PROVIDER_PRYCD)
            {
                var prycdList = await _baseRepositoryPropertiesPrycd.GetAllAsync(x => x.ImportFileId == request.FileId);
                foreach (Prycd prycd in prycdList)
                {
                    try
                    {
                        var properties = _mapper.Map<Prycd, Properties>(prycd);
                        await _baseRepositoryProperties.Create(properties);
                        successRecordCount++;
                    }
                    catch (Exception ex)
                    {
                        failedRecordCount++;
                    }
                    totalRecordCount++;
                }
            }
            propertiesFileImport.Status = "File Migrated";
            propertiesFileImport.Message = $"Total record: {totalRecordCount}. Successfully migrated: {successRecordCount}. Filed to migrate: {failedRecordCount}";
            await _baseRepositoryPropertiesFileImport.UpdateAsync(propertiesFileImport);
            return propertiesFileImport.Message;
        }
    }
}
