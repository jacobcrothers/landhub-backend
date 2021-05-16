
using Domains.DBModels;

namespace LandHubWebService.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User appicationUser);
    }
}
