
using Commands;

using Domains.DBModels;
using Domains.Dtos;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using PropertyHatchWebApi.ApplicationContext;

using System;
using System.Threading.Tasks;

namespace PropertyHatchWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {

        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMediator _mediator;
        public AccountController(ILogger<AccountController> logger
            , IMediator mediator
            , UserManager<ApplicationUser> _userManager)
        {
            _logger = logger;
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this._userManager = _userManager;
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

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult<UserForUi>> TokenExchange([FromBody] ExchangeTokenCommand exchangeTokenCommand)
        {
            exchangeTokenCommand.UserName = SecurityContext.UserName;
            var response = await _mediator.Send(exchangeTokenCommand);
            return Ok(response);
        }



        [HttpPut("[action]")]
        [Authorize]
        public ActionResult UpdateUserRole([FromBody] UpdateUserRoleCommand command)
        {
            _mediator.Send(command);
            return Ok();
        }
    }
}
