using Command;

using Domains.DBModels;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Services.Repository;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LandHubWebService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountManagerController : ControllerBase
    {

        private readonly ILogger<AccountManagerController> _logger;
        private readonly IMediator _mediator;
        private readonly IBaseRepository<User> _userBaseRepository;

        public AccountManagerController(ILogger<AccountManagerController> logger,
                                        IMediator mediator,
                                        IBaseRepository<User> userBaseRepository)
        {
            _userBaseRepository = userBaseRepository;
            _logger = logger;
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public ActionResult SaveUser([FromBody] CreateUserCommand command)
        {
            _mediator.Send(command);
            return Ok();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            var users = await _userBaseRepository.Get();
            return Ok(users);
        }
    }
}
