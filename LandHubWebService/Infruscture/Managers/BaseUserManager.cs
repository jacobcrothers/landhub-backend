
using AutoMapper;

using Domains.DBModels;

using Microsoft.AspNetCore.Identity;

using Services.IManagers;
using Services.Repository;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Managers
{
    public class BaseUserManager : IBaseUserManager
    {
        private readonly IBaseRepository<User> _userBaseRepository;
        private readonly IBaseRepository<UserRoleMapping> _userRoleMappingBaseRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly IMapper _mapper;
        public BaseUserManager(IBaseRepository<User> userBaseRepository
            , IMapper mapper
            , UserManager<ApplicationUser> userManager
             , SignInManager<ApplicationUser> signInManager
            , IBaseRepository<UserRoleMapping> userRoleMappingBaseRepository)
        {
            _userBaseRepository = userBaseRepository;
            _mapper = mapper;
            _userRoleMappingBaseRepository = userRoleMappingBaseRepository;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> RegisterUserAsync(ApplicationUser user, string password = "")
        {
            var absence = await _userManager.FindByEmailAsync(user.Email);
            if (absence != null)
            {
                return false;
            }
            var result = await _userManager.CreateAsync(user, password);

            return result.Succeeded;
        }
        public async Task<bool> Login(string email, string password = "")
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, false, lockoutOnFailure: false);
            return result.Succeeded;
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

        public async Task<ApplicationUser> FindByNameAsync(string email)
        {
            return await _userManager.FindByNameAsync(email);
        }

        public async Task<List<UserRoleMapping>> FindRolesByUserIdByOrgIdAsync(string userId, string orgId)
        {
            var data = await _userRoleMappingBaseRepository.GetAllAsync(it => it.OrganizationId == orgId && it.UserId == userId);
            return data.ToList();
        }
    }
}
