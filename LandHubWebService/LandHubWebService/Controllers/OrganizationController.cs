using Command;

using Commands;

using Domains.DBModels;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Services.IManagers;
using Services.Repository;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LandHubWebService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationController : ControllerBase
    {

        private readonly ILogger<AccountManagerController> _logger;
        private readonly IMediator _mediator;
        private readonly IBaseRepository<Organization> _organizationRepo;
        private readonly IBaseRepository<UserRoleMapping> _userRoleMapping;
        private readonly IOrganizationManager _organizationManager;
        private readonly IBaseRepository<Permission> _permissionBaseRepository;
        private readonly IBaseRepository<RolePermissionMapping> _rolePermissionMappingBaseRepository;

        public OrganizationController(ILogger<AccountManagerController> logger
            , IMediator mediator
            , IBaseRepository<UserRoleMapping> userRoleMapping
            , IBaseRepository<Organization> organizationRepo
            , IOrganizationManager organizationManager
            , IBaseRepository<Permission> permissionBaseRepository
            , IBaseRepository<RolePermissionMapping> rolePermissionMappingBaseRepository
                                       )
        {
            _userRoleMapping = userRoleMapping;
            _organizationManager = organizationManager;
            _organizationRepo = organizationRepo;
            _logger = logger;
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _permissionBaseRepository = permissionBaseRepository;
            _rolePermissionMappingBaseRepository = rolePermissionMappingBaseRepository;
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
        public async Task<Organization> GetOrgDetails(string orgId)
        {
            return await _organizationRepo.Get(orgId);
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
            List<Permission> permissions = await _permissionBaseRepository.ListAsync(x => x.IsActive == true);
            return Ok(permissions);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetPermissionByRoleIdAsync(string role)
        {
            List<RolePermissionMapping> permissions = await _rolePermissionMappingBaseRepository.ListAsync(x => x.RoleId == role);
            return Ok(permissions);
        }
    }
}
