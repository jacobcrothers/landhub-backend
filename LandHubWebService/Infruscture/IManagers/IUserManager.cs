using Domains.DBModels;

namespace Services.IManagers
{
    public interface IUserManager
    {
        void CreateUser(User user);
        User GetUserByEmail(string email);
    }
}
