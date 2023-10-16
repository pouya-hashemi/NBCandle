using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using NB.Candle.WebRzorPage.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.Services.Claims
{
    public class ClaimFactory : UserClaimsPrincipalFactory<User>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ClaimFactory(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }
        public async override Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var principal = await base.CreateAsync(user);

            ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                new Claim("FirstName", user.FirstName)
            });

            var userRoles = await _userManager.GetRolesAsync(user);


            foreach (var rolse in userRoles)
            {
                var role = await _roleManager.FindByNameAsync(rolse);
                var roleClaim = new IdentityRoleClaim<string>();
                roleClaim.RoleId = role.Id;
                roleClaim.ClaimValue = role.Name;
                roleClaim.ClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";

                ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                roleClaim.ToClaim()
            }) ;
          
            }
            //principal.Claims.ToList().Add(new Claim("FirstName", user.FirstName));
            return principal;
        }
    }

}
