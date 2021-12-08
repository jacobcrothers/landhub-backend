using Commands;
using Commands.Query;

using Domains.DBModels;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PropertyHatchWebApi.ApplicationContext;
using System;
using System.Threading.Tasks;

namespace PropertyHatchWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PcmWrapperController : BaseController
    {
        private readonly IMediator _mediator;
        public PcmWrapperController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<string> Campaign([FromBody] CampaignMailerCommand command = null)
        {
            if (command == null)
                command = new CampaignMailerCommand();
            return await _mediator.Send(command);
        }
    }
}
