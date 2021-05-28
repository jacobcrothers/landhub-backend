
using Commands;

using Domains.DBModels;
using Domains.Dtos;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PropertyHatchWebApi.ApplicationContext;

using System;
using System.Threading.Tasks;

namespace PropertyHatchWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> SaveUser([FromBody] CreateUserCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> UpdateUserInforrmation([FromBody] UserUpdateCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("[action]")]
        [Authorize]
        public async Task<ActionResult<UserForUi>> GetUserInformation()
        {
            var getUserQuery = new GetUserQuery { OrgId = SecurityContext.OrgId, UserId = SecurityContext.UserId };
            var response = await _mediator.Send(getUserQuery);
            return Ok(response);
        }

        [HttpGet("[action]")]
        [Authorize]
        public async Task<ActionResult<Organization>> GetUserOrganization()
        {
            var getUserQuery = new GetUserSpecificOrgQuery { UserId = SecurityContext.UserId };
            var response = await _mediator.Send(getUserQuery);
            return Ok(response);
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult<UserForUi>> TokenExchange([FromBody] ExchangeTokenCommand exchangeTokenCommand)
        {
            exchangeTokenCommand.UserName = SecurityContext.UserName;
            var response = await _mediator.Send(exchangeTokenCommand);
            return Ok(response);
        }
        /*
        [HttpPut("[action]")]
        [Authorize]
        public ActionResult UpdateUserRole([FromBody] UpdateUserRoleCommand command)
        {
            _mediator.Send(command);
            return Ok();
        }
        */

        [HttpPost("[action]")]
        [Authorize]
        public ActionResult InviteUser([FromBody] SendInvitationCommand command)
        {
            command.UserId = SecurityContext.UserId;
            command.UserDisplayName = SecurityContext.DisplayName;
            command.OrgId = SecurityContext.OrgId;
            command.OrgName = SecurityContext.OrgName;
            _mediator.Send(command);
            return Ok();
        }
    }
}
