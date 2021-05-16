using Command;

using Commands;

using Domains.DBModels;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Services.Repository;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LandHubWebService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountManagerController : ControllerBase
    {

        private readonly ILogger<AccountManagerController> _logger;
        private readonly IMediator _mediator;
        private readonly IBaseRepository<User> _userBaseRepository;
        private readonly IBaseRepository<UserRoleMapping> _userRoleMappingBaseRepository;

        public AccountManagerController(ILogger<AccountManagerController> logger,
                                        IMediator mediator,
                                        IBaseRepository<User> userBaseRepository,
                                        IBaseRepository<UserRoleMapping> userRoleMappingBaseRepository)
        {
            _userBaseRepository = userBaseRepository;
            _logger = logger;
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _userRoleMappingBaseRepository = userRoleMappingBaseRepository;
        }

        [HttpPost("[action]")]
        public ActionResult SaveUser([FromBody] CreateUserCommand command)
        {
            _mediator.Send(command);
            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<User>>> Get(GetUserQuery getUserQuery)
        {
            if (getUserQuery.UserId != null && getUserQuery.OrgId != null)
            {
                var user = await _userBaseRepository.Get(getUserQuery.UserId);
                var rolesMapping = await _userRoleMappingBaseRepository.ListAsync(x => x.OrganizationId == getUserQuery.OrgId && x.UserId == getUserQuery.UserId);
                List<string> rolesId = new List<string>();
                foreach (UserRoleMapping rolePermissionMapping in rolesMapping)
                {
                    rolesId.Add(rolePermissionMapping.Id);
                }
                user.Roles = rolesId;
                return Ok(user);
            }
            return BadRequest("please provide user id and OrgId");
        }


        [HttpPut("[action]")]
        public ActionResult UpdateUserRole([FromBody] UpdateUserRoleCommand command)
        {
            _mediator.Send(command);
            return Ok();

        }
    }
}
