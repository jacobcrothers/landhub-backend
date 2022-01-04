
using Commands;
using Commands.Query;

using Domains.DBModels;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading.Tasks;

namespace PropertyHatchWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DueDeligenceController : BaseController
    {
        private readonly IMediator _mediator;
        public DueDeligenceController(IMediator _mediator)
        {
            this._mediator = _mediator;
        }
        
        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> Add([FromBody] CreateDueDeligenceCommand createDueDeligenceCommand)
        {
            createDueDeligenceCommand.OrgId = SecurityContext.OrgId;
            createDueDeligenceCommand.CreatedDate = DateTime.UtcNow;
            await _mediator.Send(createDueDeligenceCommand);
            return Ok();
        }

        [HttpGet("[action]")]
        [Authorize]
        public async Task<ActionResult> GetByAPN(string apn)
        {
            var getDueDeligenceQuery = new GetDueDeligenceQuery
            {
                OrgId = SecurityContext.OrgId,
                apn = apn
            };

            var result = await _mediator.Send(getDueDeligenceQuery);
            return Ok(result);
        }
    }
}
