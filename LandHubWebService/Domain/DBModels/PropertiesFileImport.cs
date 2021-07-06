using System;
using System.Collections.Generic;

namespace Domains.DBModels
{
    public class PropertiesFileImport : BaseEntity
    {
        public string FileName { get; set; }
        public string FileContent { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string OrgId { get; set; }
        public DateTime UploadDate { get; set; }
        public KeyValuePair<string, string> ColumnMapping { get; set; }
        public string Status { get; set; }
    }
}
