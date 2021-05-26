﻿using Domains.DBModels;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using Newtonsoft.Json;

using Services.IManagers;
using Services.IServices;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:key"]));
            this.userManager = userManager;
            this.organizationManager = organizationManager;
        }

        public async Task<string> CreateTokenAsync(ApplicationUser appicationUser)
        {
            var org = await organizationManager.GetSingleOrganizationByCreatorAsync(appicationUser.Id);
            var userRoleMapping = await userManager.FindRolesByUserIdByOrgIdAsync(appicationUser.Id, org.Id);
            List<Claim> claims = await PrepareClaimsAsync(appicationUser, org, userRoleMapping);
            var token = GenerateToken(claims);
            return token;
        }
        public async Task<string> ExchangeTokenAsync(ApplicationUser appicationUser, string orgId)
        {
            var userRoleMapping = await userManager.FindRolesByUserIdByOrgIdAsync(appicationUser.Id, orgId);
            if (userRoleMapping == null || userRoleMapping.Count == 0)
                return string.Empty;

            var org = await organizationManager.GetSingleOrganizationByIdAsync(orgId);
            List<Claim> claims = await PrepareClaimsAsync(appicationUser, org, userRoleMapping);

            if (claims == null)
                return string.Empty;

            var token = GenerateToken(claims);
            return token;
        }


        private async Task<List<Claim>> PrepareClaimsAsync(ApplicationUser applicationUser, Organization org, List<UserRoleMapping> userRoleMapping)
        {
            List<string> list = new List<string>();
            List<string> permission = new List<string>();
            if (applicationUser == null && org == null && userRoleMapping == null && userRoleMapping.Count == 0)
            {
                return null;
            }

            foreach (UserRoleMapping roleMapping in userRoleMapping)
            {
                list.Add(roleMapping.RoleId);
                var permissionMapping = await userManager.FindRolesPermissionMappingByUserIdByOrgIdAsync(roleMapping.RoleId, org.Id);
                permission.AddRange(permissionMapping.Select(x => x.PermissionKey));
            }

            var claims = new List<Claim>{
                new Claim(ClaimTypes.Email, applicationUser.Email),
                new Claim(ClaimTypes.GivenName, applicationUser.DisplayName),
                new Claim("DisplayName", applicationUser.DisplayName),
                new Claim("UserName", applicationUser.Email),
                new Claim("UserId", applicationUser.Id),
                new Claim("OrgId", org.Id),
                new Claim("OrgName", org.Title),
                new Claim("Permissions", JsonConvert.SerializeObject(permission)),
                new Claim("Roles", JsonConvert.SerializeObject(list)),
            };
            return claims;
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
