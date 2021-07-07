
using Commands;

using Domains.DBModels;
using Domains.Dtos;

using Infrastructure;

using MediatR;

using Services.Repository;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandler
{
    public class MapPropertiesColumnCommandHandler : IRequestHandler<MapPropertiesColumnCommand, ColumnMapResult>
    {

        private IBaseRepository<PhatchConfiguration> _baseRepositoryPropertyHatchConfiguration;
        private IBaseRepository<PropertiesFileImport> _baseRepositoryPropertiesFileImport;

        public MapPropertiesColumnCommandHandler(IBaseRepository<PhatchConfiguration> baseRepositoryPropertyHatchConfiguration
            , IBaseRepository<PropertiesFileImport> baseRepositoryPropertiesFileImport)
        {
            _baseRepositoryPropertyHatchConfiguration = baseRepositoryPropertyHatchConfiguration;
            _baseRepositoryPropertiesFileImport = baseRepositoryPropertiesFileImport;
        }

        public async Task<ColumnMapResult> Handle(MapPropertiesColumnCommand request, CancellationToken cancellationToken)
        {
            var propertiesFileImport = await _baseRepositoryPropertiesFileImport.GetSingleAsync(x => x.Id == request.FileId);

            var dbColumnStatus = new List<DbColumnStatus>();
            var columnDisplayNames = File.ReadLines(@"E:\Kingsville20TX20.csv").First().Split(',');

            if (propertiesFileImport.Extension.ToLower() == Const.PROPERTY_LIST_IMPORT_FILE_TYPE_CSV)
            {
                var fileContent = System.Text.Encoding.UTF8.GetString(propertiesFileImport.FileContent).Split(
                    new[] { "\r\n", "\r", "\n" },
                    StringSplitOptions.None
                );

                if (request.ListProvider.ToLower() == Const.PROPERTY_LIST_PROVIDER_AGENT_PRO)
                {
                    columnDisplayNames = fileContent.First().Split(',');
                    var propertyConfig = await _baseRepositoryPropertyHatchConfiguration.GetSingleAsync(x => x.ConfigKey == $"{Const.PROPERTY_LIST_PROVIDER_AGENT_PRO}_{Const.PROPERTY_LIST_IMPORT_FILE_TYPE_CSV}");

                    var propertyList = (IList)propertyConfig.ConfigValue;
                    foreach (dynamic data in propertyList)
                    {
                        IDictionary<string, object> propertyValues = data;
                        dbColumnStatus.Add(new DbColumnStatus
                        {
                            ColumnName = propertyValues["ColumnName"].ToString(),
                            DisplayName = propertyValues["DisplayName"].ToString(),
                            IsMapped = columnDisplayNames.Contains(propertyValues["DisplayName"].ToString())
                        });
                    }
                }
            }

            var columnMapResult = new ColumnMapResult
            {
                CollumnsInCsv = columnDisplayNames,
                DbColumnsStatus = dbColumnStatus
            };

            return columnMapResult;
        }

    }
}
