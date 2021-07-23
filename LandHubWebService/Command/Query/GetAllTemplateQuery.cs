using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.DBModels;
using MediatR;

namespace Commands.Query
{
    public class GetAllTemplateQuery : IRequest<List<DocumentTemplate>>
    {
    }
}
