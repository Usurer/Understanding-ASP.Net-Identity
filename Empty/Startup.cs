using System;
using System.Threading.Tasks;
using Empty.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartupAttribute(typeof(Empty.Startup))]
namespace Empty
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<MyUserManager>(() => new MyUserManager(new MyUserStore()));
            app.CreatePerOwinContext<MySignInManager>(MySignInManager.Create);

            ConfigureAuth(app);
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Login/Index"),
                Provider = new MyCookieAuthenticationProvider
                {
                    OnValidateIdentity = /*MyTimeStampValidator.OnValidateIdentity<MyUserManager, MyUser>(
                            validateInterval: TimeSpan.FromSeconds(1),
                            regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))*/

                    (x) =>
                    {
                        var userManager = x.OwinContext.GetUserManager<MyUserManager>();

                        var userId = x.Identity.GetUserId();
                        var foundUser = userManager.FindById(userId);
                        if (foundUser != null)
                        {
                            x.OwinContext.Authentication.SignIn(x.Properties, x.Identity);
                        }
                        else
                        {
                            x.OwinContext.Authentication.SignOut();
                            x.RejectIdentity();
                        }

                        return Task.CompletedTask;
                    }
                }
                /*Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = MyTimeStampValidator.OnValidateIdentity<MyUserManager, MyUser>(
                        validateInterval: TimeSpan.FromSeconds(1),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }*/
            });
        }
    }
}