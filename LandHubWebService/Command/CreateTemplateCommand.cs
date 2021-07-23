using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Commands
{
    public class CreateTemplateCommand : IRequest
    {
        public string TemplateName { get; set; }
        public string TemplateData { get; set; }

        public string TemplateType { get; set; }

        public string CreatedBy { get; set; }

        public string CreatedAt { get; set; }

        public string Status { get; set; }

    }
}
