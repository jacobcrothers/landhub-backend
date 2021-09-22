using Commands;
using Commands.Query;

using Domains.DBModels;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PropertyHatchWebApi.ApplicationContext;

using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PropertyHatchWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateController : BaseController
    {
        private readonly IMediator _mediator;
        public TemplateController(IMediator mediator)
        {
            this._mediator = mediator;
        }


        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> GetAllTemplates([FromBody] GetAllTemplateQuery getAllTemplateQuery)
        {
            getAllTemplateQuery.OrganizationId = SecurityContext.OrgId;
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

        [HttpGet("[action]")]
        [Authorize]
        public async Task<ActionResult> GetTotalCount()
        {
            var getCountQuery = new GetCountQuery()
            {
                OrganizationId = SecurityContext.OrgId,
                EntityName = typeof(DocumentTemplate)
            };
            var result = await _mediator.Send(getCountQuery);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> CreateTemplate([FromBody] CreateTemplateCommand createTemplateCommand)
        {
            createTemplateCommand.OrganizationId = SecurityContext.OrgId;
            createTemplateCommand.CreatedBy = SecurityContext.UserId;
            var result = await _mediator.Send(createTemplateCommand);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> UpdateTemplate([FromBody] UpdateTemplateCommand updateTemplateCommand)
        {
            updateTemplateCommand.OrganizationId = SecurityContext.OrgId;
            var result = await _mediator.Send(updateTemplateCommand);
            return Ok(result);
        }

    }
}
