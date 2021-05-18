using Command;

using Commands;

using Domains.DBModels;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Services.IManagers;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LandHubWebService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {

        private readonly ILogger<AccountController> _logger;
        private readonly IMediator _mediator;
        private readonly IBaseUserManager baseUserManager;
        public AccountController(ILogger<AccountController> logger
            , IMediator mediator
            , IBaseUserManager baseUserManager)
        {
            _logger = logger;
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.baseUserManager = baseUserManager;
        }

        [HttpPost("[action]")]
        public ActionResult SaveUser([FromBody] CreateUserCommand command)
        {
            _mediator.Send(command);
            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<User>>> Get(GetUserQuery getUserQuery)
        {
            var response = _mediator.Send(getUserQuery);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> RegisterAsync([FromBody] CreateUserCommand command)
        {
            await _mediator.Send(command);
            return Ok();

        }

        [HttpPut("[action]")]
        public ActionResult UpdateUserRole([FromBody] UpdateUserRoleCommand command)
        {
            _mediator.Send(command);
            return Ok();
        }
    }
}
