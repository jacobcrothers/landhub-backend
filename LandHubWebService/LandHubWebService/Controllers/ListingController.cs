
using Commands;
using Commands.Query;
using Domains.DBModels;
using MediatR;

using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Services.Repository;
using System.Threading.Tasks;

namespace PropertyHatchWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListingController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMongoLandHubDBContext _mongoContext;
        private readonly IMongoCollection<Listing> _dbCollection;
        public ListingController(IMediator _mediator, IMongoLandHubDBContext context)
        {
            this._mediator = _mediator;
            //_mongoContext = context;


        //_dbCollection = _mongoContext.GetCollection<Listing>("Listing");
        //    //_dbCollection.UpdateMany<Listing>(x => true, Builders<Listing>.Update.Set(x => x.OrganizationId, "6bdc1f5c-482e-4ee9-95d8-24d4011025ce"));
        //    _dbCollection.DeleteManyAsync(x=>true);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetAll(string orgId)
        {
            var getAllListingQuery = new GetAllListingQuery
            {
                OrgId = orgId
            };

            var result = await _mediator.Send(getAllListingQuery);
            return Ok(result);
        }

        [HttpGet("[action]")]
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
        public async Task<ActionResult> Add([FromBody] CreateListingCommand createListingCommand)
        {/*
            createListingCommand.OrganizationId = SecurityContext.OrgId;*/
            await _mediator.Send(createListingCommand);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Update([FromBody] UpdateListingCommand updateListingCommand)
        {
            await _mediator.Send(updateListingCommand);
            return Ok();
        }


        [HttpPost("[action]")]
        public async Task<ActionResult> Delete([FromBody] DeleteListingCommand deleteListingCommand)
        {
            await _mediator.Send(deleteListingCommand);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> UpdatePropertiesResource([FromBody] ListingResourceUpdateCommand listingResourceUpdateCommand)
        {
            await _mediator.Send(listingResourceUpdateCommand);
            return Ok();
        }

    }
}
