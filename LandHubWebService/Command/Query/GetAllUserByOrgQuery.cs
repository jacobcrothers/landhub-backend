﻿using Domains.Dtos;

using MediatR;

using System.Collections.Generic;

namespace Commands.Query
{
    public class GetAllUserByOrgQuery : IRequest<List<UserForUi>>
    {
        public string OrgId { get; set; }

    }
}
