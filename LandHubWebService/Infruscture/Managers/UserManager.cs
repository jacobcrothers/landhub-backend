
using AutoMapper;

using Domains.DBModels;

using Services.IManagers;
using Services.Repository;

namespace Services.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IBaseRepository<User> _userBaseRepository;

        private readonly IMapper _mapper;
        public UserManager(IBaseRepository<User> userBaseRepository,
                           IMapper mapper)
        {
            _userBaseRepository = userBaseRepository;
            _mapper = mapper;
        }
        public void CreateUser(User user)
        {
            _userBaseRepository.Create(user);
        }

        public User GetUserByEmail(string email)
        {
            return new User();
        }
    }
}
