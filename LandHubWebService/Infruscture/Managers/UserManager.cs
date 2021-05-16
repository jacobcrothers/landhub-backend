
using AutoMapper;

using Domains.DBModels;

using Services.IManagers;
using Services.Repository;

using System.Collections.Generic;

namespace Services.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IBaseRepository<User> _userBaseRepository;
        private readonly IBaseRepository<UserRoleMapping> _userRoleMappingBaseRepository;

        private readonly IMapper _mapper;
        public UserManager(IBaseRepository<User> userBaseRepository
            , IMapper mapper
            , IBaseRepository<UserRoleMapping> userRoleMappingBaseRepository)
        {
            _userBaseRepository = userBaseRepository;
            _mapper = mapper;
            _userRoleMappingBaseRepository = userRoleMappingBaseRepository;
        }
        public void CreateUser(User user)
        {
            _userBaseRepository.Create(user);
        }

        public User GetUserByEmail(string email)
        {
            return new User();
        }
        public void UpdateUserRoleOrgMaps(List<UserRoleMapping> userRoleMappings)
        {
            foreach (UserRoleMapping userRoleMapping in userRoleMappings)
            {
                _userRoleMappingBaseRepository.Create(userRoleMapping);
            }
        }
    }
}
