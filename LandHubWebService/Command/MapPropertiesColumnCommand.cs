using Domains.Dtos;

using MediatR;

using System.Collections.Generic;

namespace Commands
{
    public class MapPropertiesColumnCommand : IRequest<List<ColumnMapResult>>
    {
        public string FileExtension { get; set; }
        public string ListProvider { get; set; }
        public string PropertyType { get; set; }
    }
}
