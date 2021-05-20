
using Commands;

using Domains.DBModels;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Services.IManagers;
using Services.Repository;

using System;
using System.Threading.Tasks;

namespace LandHubWebService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationController : ControllerBase
    {

        private readonly ILogger<AccountController> _logger;
        private readonly IMediator _mediator;
        private readonly IBaseRepository<Organization> _organizationRepo;

        public OrganizationController(ILogger<AccountController> logger
            , IMediator mediator
            , IBaseRepository<UserRoleMapping> userRoleMapping
            , IBaseRepository<Organization> organizationRepo
            , IOrganizationManager organizationManager
            , IBaseRepository<Permission> permissionBaseRepository
            , IBaseRepository<RolePermissionMapping> rolePermissionMappingBaseRepository
                                       )
        {
            _organizationRepo = organizationRepo;
            _logger = logger;
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("[action]")]
        [Authorize]
        public async Task<ActionResult> GetOrgDetail(string orgId)
        {
            var result = await _mediator.Send(new GetOrgQuery { OrgId = orgId });
            return Ok(result);
        }


        /*

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
                return await _userRoleMapping.GetAllAsync(x => x.OrganizationId == orgId);
            else return null;
        }

       
        [HttpPost("[action]")]
        public ActionResult CreateRole([FromBody] CreateRoleCommand createRoleCommand)
        {
            _mediator.Send(createRoleCommand);
            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetAllPermissionAsync()
        {
            List<Permission> permissions = await _permissionBaseRepository.GetAllAsync(x => x.IsActive == true);
            return Ok(permissions);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetPermissionByRoleIdAsync(string role)
        {
            List<RolePermissionMapping> permissions = await _rolePermissionMappingBaseRepository.GetAllAsync(x => x.RoleId == role);
            return Ok(permissions);
        }
        */
    }
}
