using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital_System.Models;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Hospital_System
{
    public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser,IdentityRole>
    {

        public ApplicationUserClaimsPrincipalFactory(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options)
            : base(userManager, roleManager, options)
        { }

        protected override async  Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
           var identity =  await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("UserFirstName", user.UserName ?? ""));
            //identity.AddClaim(new Claim("UserLastName", user.LastName ?? ""));
            //identity.AddClaim(new Claim("Gender", user. ?? ""));
            identity.AddClaim(new Claim("Email", user.Email ?? ""));


            return identity;

        }
    }
}
