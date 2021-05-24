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
    public class AuthorizationController : BaseController
    {
        private readonly IMediator _mediator;
        public AuthorizationController(IMediator _mediator)
        {
            this._mediator = _mediator;
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> GetRole()
        {
            var query = new GetRoleQuery
            {
                OrgId = SecurityContext.OrgId
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public ActionResult CreateRole([FromBody] CreateRoleCommand createRoleCommand)
        {
            createRoleCommand.OrgId = SecurityContext.OrgId;
            _mediator.Send(createRoleCommand);
            return Ok();
        }

    }
}
