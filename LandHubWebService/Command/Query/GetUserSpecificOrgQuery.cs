using Domains.DBModels;
using Domains.Dtos;

using MediatR;

using System.Collections.Generic;

namespace Commands
{
    public class GetUserSpecificOrgQuery : IRequest<List<Organization>>
    {
        public string UserId { get; set; }

    }
}
