
using Commands;
using Commands.Query;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace PropertyHatchWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesWebsiteController : BaseController
    {
        private readonly IMediator _mediator;
        public SalesWebsiteController(IMediator _mediator)
        {
            this._mediator = _mediator;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetAll(string orgId)
        {
            var getAllSalesWebsiteQuery = new GetAllSalesWebsiteQuery
            {
                OrgId = orgId
            };

            var result = await _mediator.Send(getAllSalesWebsiteQuery);
            return Ok(result);
        }       

        [HttpGet("[action]")]
        public async Task<ActionResult> GetById(string saleswebsiteId)
        {
            var getSalesWebsiteQuery = new GetSalesWebsiteQuery
            {
                SaleswebsiteId = saleswebsiteId ?? "1"
            };

            var result = await _mediator.Send(getSalesWebsiteQuery);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Add([FromBody] CreateSalesWebsiteCommand createSalesWebsiteCommand)
        {/*
            createListingCommand.OrganizationId = SecurityContext.OrgId;*/
            await _mediator.Send(createSalesWebsiteCommand);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Update([FromBody] UpdateSalesWebsiteCommand updateSalesWebsiteCommand)
        {
            await _mediator.Send(updateSalesWebsiteCommand);
            return Ok();
        }


        [HttpPost("[action]")]
        public async Task<ActionResult> Delete([FromBody] DeleteSalesWebsiteCommand deleteSalesWebsiteCommand)
        {
            await _mediator.Send(deleteSalesWebsiteCommand);
            return Ok();
        }

    }
}
