using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Ids4.SSO.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ids4.SSO.Services
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<AppUser> _userManager;

        public ProfileService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        private async Task<List<Claim>> GetClaimsFromUserAsync(AppUser user)
        {
            if(user == null)
            {
                return null;
            }

            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Subject,user.Id.ToString()),
                new Claim(JwtClaimTypes.PreferredUserName,user.UserName),
                new Claim(JwtClaimTypes.Email,user.Email)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(JwtClaimTypes.Role, role));
            }

            if(!string.IsNullOrWhiteSpace(user.Avatar))
            {
                claims.Add(new Claim("avatar", user.Avatar));
            }

            return claims;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var subjectCalim = context.Subject.Claims.FirstOrDefault(c => c.Type == "sub");
            if(subjectCalim != null)
            {
                var user = await _userManager.FindByIdAsync(subjectCalim.Value);
                context.IssuedClaims = await GetClaimsFromUserAsync(user);
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var subjectCalim = context.Subject.Claims.FirstOrDefault(c => c.Type == "sub");
            if(subjectCalim == null)
            {
                context.IsActive = false;
            }
            else
            {
                var user = await _userManager.FindByIdAsync(subjectCalim.Value);
                context.IsActive = user != null;
            }
        }
    }
}
