using System.Collections.Generic;

namespace Domains.Dtos
{
    public class ColumnMapResult
    {
        public List<DbColumnStatus> DdColumnsStatus { get; set; }
        public string[] CollumnsInCsv { get; set; }
    }

    public class CollumnsInCsv
    {
        public string[] Columns { get; set; }
    }

    public class DbColumnStatus
    {
        public string ColumnName { get; set; }
        public string DisplayName { get; set; }
        public bool IsMapped { get; set; }
    }

}
