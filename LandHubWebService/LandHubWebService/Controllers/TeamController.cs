
using Commands;
using Commands.Query;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace PropertyHatchWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamController : BaseController
    {
        private readonly IMediator _mediator;
        public TeamController(IMediator _mediator)
        {
            this._mediator = _mediator;
        }

        [HttpGet("[action]")]
        [Authorize]
        public async Task<ActionResult> GetAll([FromBody] GetAllTeamQuery getAllTeamQuery)
        {
            getAllTeamQuery.OrgId = SecurityContext.OrgId;
            var result = await _mediator.Send(getAllTeamQuery);
            return Ok(result);
        }

        [HttpGet("[action]")]
        [Authorize]
        public async Task<ActionResult> GetById(string teamId)
        {
            var getTeamQuery = new GetTeamQuery
            {
                OrgId = SecurityContext.OrgId,
                TeamId = teamId
            };

            var result = await _mediator.Send(getTeamQuery);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> Add([FromBody] CreateTeamCommand createTeamCommand)
        {
            createTeamCommand.OrganizationId = SecurityContext.OrgId;
            await _mediator.Send(createTeamCommand);
            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> Update([FromBody] UpdateTeamCommand updateTeamCommand)
        {
            if (updateTeamCommand.OrganizationId == SecurityContext.OrgId)
            {
                await _mediator.Send(updateTeamCommand);
            }

            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> Delete([FromBody] DeleteTeamCommand deleteListingCommand)
        {
            await _mediator.Send(deleteListingCommand);
            return Ok();
        }

    }
}
