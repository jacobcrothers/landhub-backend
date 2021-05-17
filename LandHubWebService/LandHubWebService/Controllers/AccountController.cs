using Command;

using Commands;

using Domains.DBModels;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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

        public AccountController(ILogger<AccountController> logger,
                                        IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
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
        public ActionResult Register([FromBody] CreateUserCommand command)
        {
            _mediator.Send(command);
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
