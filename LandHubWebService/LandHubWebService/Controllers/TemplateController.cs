using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commands;
using Commands.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PropertyHatchWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TemplateController(IMediator mediator)
        {
            this._mediator = mediator;
        }


        [HttpPost("[action]")]
        public async Task<ActionResult> GetAllTemplates([FromBody] GetAllTemplateQuery getAllTemplateQuery)
        {
            var result = await _mediator.Send(getAllTemplateQuery);
            return Ok(result);

        }


        //[HttpPost("[action]")]
        //[Authorize]
        //public async Task<ActionResult> GetAll([FromBody] GetAllPropertiesQuery getAllPropertiesQuery)
        //{
        //    getAllPropertiesQuery.OrgId = SecurityContext.OrgId;
        //    var result = await _mediator.Send(getAllPropertiesQuery);
        //    return Ok(result);
        //}

        [HttpPost("[action]")]
        public async Task<ActionResult> CreateTemplate([FromBody] CreateTemplateCommand createTemplateCommand)
        {
            var result = await _mediator.Send(createTemplateCommand);
            return Ok(result);

        }

    }
}
