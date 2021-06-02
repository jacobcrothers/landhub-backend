
using Commands;
using Commands.Query;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace PropertyHatchWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListingController : BaseController
    {
        private readonly IMediator _mediator;
        public ListingController(IMediator _mediator)
        {
            this._mediator = _mediator;
        }

        [HttpGet("[action]")]
        [Authorize]
        public async Task<ActionResult> GetAll()
        {
            var getAllListingQuery = new GetAllListingQuery
            {
                OrgId = SecurityContext.OrgId
            };

            var result = await _mediator.Send(getAllListingQuery);
            return Ok(result);
        }

        [HttpGet("[action]")]
        [Authorize]
        public async Task<ActionResult> GetById(string listingId)
        {
            var getListingQuery = new GetListingQuery
            {
                ListingId = listingId ?? "1"
            };

            var result = await _mediator.Send(getListingQuery);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> Add([FromBody] CreateListingCommand createListingCommand)
        {
            createListingCommand.OrganizationId = SecurityContext.OrgId;
            await _mediator.Send(createListingCommand);
            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> Update([FromBody] UpdateListingCommand updateListingCommand)
        {
            await _mediator.Send(updateListingCommand);
            return Ok();
        }

    }
}
