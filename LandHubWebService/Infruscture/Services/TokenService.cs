using Domains.DBModels;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using Newtonsoft.Json;

using Services.IManagers;
using Services.IServices;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        private readonly IBaseUserManager userManager;
        private readonly IOrganizationManager organizationManager;

        public TokenService(IConfiguration configuration
            , IBaseUserManager userManager
            , IOrganizationManager organizationManager)
        {
            _config = configuration;
            var hle = _config["Token:key"];
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:key"]));
            this.userManager = userManager;
            this.organizationManager = organizationManager;
        }

        public async Task<string> CreateTokenAsync(ApplicationUser appicationUser)
        {
            var org = await organizationManager.GetSingleOrganizationByCreatorAsync(appicationUser.Id);
            var userRoleMapping = await userManager.FindRolesByUserIdByOrgIdAsync(appicationUser.Id, org.Id);
            List<string> list = new List<string>();

            foreach (UserRoleMapping roleMapping in userRoleMapping)
            {
                list.Add(roleMapping.RoleId);
            }

            var claims = new List<Claim>{
                new Claim(ClaimTypes.Email, appicationUser.Email),
                new Claim(ClaimTypes.GivenName, appicationUser.DisplayName),
                new Claim("Roles", JsonConvert.SerializeObject(list)),
                new Claim("OrgId", org.Id)
            };

            var token = GenerateToken(claims);
            return token;
        }


        private string GenerateToken(List<Claim> claims)
        {
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _config["Token:Issuer"]
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
