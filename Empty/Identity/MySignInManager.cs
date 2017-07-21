using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace Empty.Identity
{
    public class MySignInManager : SignInManager<MyUser, string>
    {
        public MySignInManager(MyUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        //public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        //{
        //    return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        //}

        public static MySignInManager Create(IdentityFactoryOptions<MySignInManager> options, IOwinContext context)
        {
            return new MySignInManager(context.GetUserManager<MyUserManager>(), context.Authentication);
        }
    }
}