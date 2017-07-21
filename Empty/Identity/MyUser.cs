using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;

namespace Empty.Identity
{
    public class MyUser : IUser
    {
        public string Id  { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(MyUserManager manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}