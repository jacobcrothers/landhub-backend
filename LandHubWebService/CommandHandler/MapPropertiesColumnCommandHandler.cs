
using Commands;

using Domains.DBModels;
using Domains.Dtos;

using Infruscture;

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
    public class MapPropertiesColumnCommandHandler : IRequestHandler<MapPropertiesColumnCommand, List<ColumnMapResult>>
    {

        private IBaseRepository<PropertyhatchConfiguration> _baseRepositoryPropertyHatchConfiguration;

        public MapPropertiesColumnCommandHandler(IBaseRepository<PropertyhatchConfiguration> baseRepositoryPropertyHatchConfiguration)
        {
            _baseRepositoryPropertyHatchConfiguration = baseRepositoryPropertyHatchConfiguration;
        }

        public async Task<List<ColumnMapResult>> Handle(MapPropertiesColumnCommand request, CancellationToken cancellationToken)
        {
            var columnMapList = new List<ColumnMapResult>();
            if (request.FileExtension.ToLower() == Const.PROPERTY_LIST_IMPORT_FILE_TYPE)
            {
                if (request.ListProvider.ToLower() == Const.PROPERTY_LIST_PROVIDER_AGENT_PRO)
                {
                    var propertyConfig = await _baseRepositoryPropertyHatchConfiguration.GetSingleAsync(x => x.ConfigKey == $"{Const.PROPERTY_LIST_PROVIDER_AGENT_PRO}_{Const.PROPERTY_LIST_IMPORT_FILE_TYPE}");
                    var columnDisplayNames = File.ReadLines(@"E:\Kingsville20TX20.csv").First().Split(',');
                    var propertyList = (IList)propertyConfig.ConfigValue;
                    foreach (dynamic data in propertyList)
                    {
                        IDictionary<string, object> propertyValues = data;
                        columnMapList.Add(new ColumnMapResult
                        {
                            ColumnName = propertyValues["ColumnName"].ToString(),
                            DisplayName = propertyValues["DisplayName"].ToString(),
                            IsMapped = columnDisplayNames.Contains(propertyValues["DisplayName"].ToString())
                        });
                    }


                }
            }

            return columnMapList;
        }

    }
}
