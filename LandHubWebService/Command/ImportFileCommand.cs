using MediatR;

using System;

namespace Commands
{
    public class ImportFileCommand : IRequest<string>
    {
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        public string Extension { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string OrgId { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
