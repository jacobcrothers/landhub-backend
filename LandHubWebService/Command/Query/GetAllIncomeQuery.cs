﻿using Domains.Dtos;

using MediatR;

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Commands.Query
{
    public class GetAllIncomeQuery : Pagination, IRequest<List<IncomeForUi>>
    {
        [JsonIgnore]
        public string OrgId { get; set; }
    }
}
