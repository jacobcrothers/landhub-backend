using Command;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;

namespace LandHubWebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountManagerController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMediator _mediator;


        public AccountManagerController(ILogger<WeatherForecastController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public ActionResult Get()
        {
            CreateUserCommand command = new CreateUserCommand();
            command.Name = "Rokon";
            _mediator.Send(command);
            return Ok();
        }
    }
}
