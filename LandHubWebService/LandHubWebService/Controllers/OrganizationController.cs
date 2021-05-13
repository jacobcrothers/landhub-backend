using Command;
using Domains.DBModels;
using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.IManagers;
using Services.Managers;
using Services.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LandHubWebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrganizationController: ControllerBase
    {

        private readonly ILogger<AccountManagerController> _logger;
        private readonly IMediator _mediator;
        private readonly IBaseRepository<Organization> _organizationRepo;
        private readonly IBaseRepository<UserRoleMapping> _userRoleMapping;
        private readonly IOrganizationManager _organizationManager;

        public OrganizationController(ILogger<AccountManagerController> logger,
                                       IMediator mediator,
                                       IBaseRepository<UserRoleMapping> userRoleMapping,
                                       IBaseRepository<Organization> organizationRepo,
                                       IOrganizationManager organizationManager
                                       )
        {
            _userRoleMapping = userRoleMapping;
            _organizationManager = organizationManager; 
            _organizationRepo = organizationRepo;
            _logger = logger;
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        [HttpPost("[action]")]
        public ActionResult CreateOrganization([FromBody] CreateNewUserWithOrgCommand command)
        {
            _mediator.Send(command);
            return Ok();
        }
        [HttpGet("[action]")]
        public async Task<List<UserRoleMapping>> GetRoleDetailsWithOrgId(string orgId)
        {
            if (orgId != null)
                return await _userRoleMapping.ListAsync(x => x.OrganizationId == orgId);
            else return null; 
        }
        [HttpGet("[action]")]
        public async Task<IEnumerable<UserRoleMapping>> GetDetails()
        {
                return await _userRoleMapping.Get();
        }
        
    }
}
