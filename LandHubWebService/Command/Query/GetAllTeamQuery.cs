﻿using Domains.Dtos;

using MediatR;

using System.Collections.Generic;

namespace Commands.Query
{
    public class GetAllTeamQuery : IRequest<List<TeamForUi>>
    {
        public string OrgId { get; set; }

    }
}
