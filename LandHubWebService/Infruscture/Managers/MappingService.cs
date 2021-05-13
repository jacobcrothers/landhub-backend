using AutoMapper;
using Domains;
using Domains.DBModels;
using MongoDB.Bson;
using MongoDB.Driver;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Repository
{
    public  class MappingService : IMappingService
    {
        private readonly IBaseRepository<UserRoleMapping> _userRoleMappingRepo;
        private readonly IMapper _mapper;

        public MappingService(IBaseRepository<UserRoleMapping> userRoleMappingRepo
                              ,IMapper mapper)
        {
            _userRoleMappingRepo = userRoleMappingRepo;
            _mapper = mapper;
        }

        public async Task MapUserOrgRole(string roleId, string userId, string organizationId)
        {

            var userRoleMap = new UserRoleMapping
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                RoleId = roleId,
                OrganizationId = organizationId
            };
             await _userRoleMappingRepo.Create(userRoleMap);
        }
    }
}
