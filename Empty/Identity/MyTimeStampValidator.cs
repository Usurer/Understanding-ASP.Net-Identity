using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Cookies;

namespace Empty.Identity
{
    // Should be MySecurityStampValidator
    public class MyTimeStampValidator
    {
        public static Func<CookieValidateIdentityContext, Task> OnValidateIdentity<TManager, TUser>(TimeSpan validateInterval, Func<TManager, TUser, Task<ClaimsIdentity>> regenerateIdentity) where TManager : UserManager<TUser, string> where TUser : class, IUser<string>
        {
            return OnValidateIdentity<TManager, TUser, string>(validateInterval, regenerateIdentity, (Func<ClaimsIdentity, string>)(id => id.GetUserId()));
        }

        public static Func<CookieValidateIdentityContext, Task> OnValidateIdentity<TManager, TUser, TKey>(TimeSpan validateInterval, Func<TManager, TUser, Task<ClaimsIdentity>> regenerateIdentityCallback, Func<ClaimsIdentity, TKey> getUserIdCallback) where TManager : UserManager<TUser, TKey> where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
        {
            if (getUserIdCallback == null)
                throw new ArgumentNullException("getUserIdCallback");
            return (Func<CookieValidateIdentityContext, Task>)(async context =>
            {
                DateTimeOffset currentUtc = DateTimeOffset.UtcNow;
                if (context.Options != null && context.Options.SystemClock != null)
                    currentUtc = context.Options.SystemClock.UtcNow;
                DateTimeOffset? issuedUtc = context.Properties.IssuedUtc;
                bool validate = !issuedUtc.HasValue;
                if (issuedUtc.HasValue)
                    validate = currentUtc.Subtract(issuedUtc.Value) > validateInterval;
                if (!validate)
                    return;
                TManager manager = context.OwinContext.GetUserManager<TManager>();
                TKey userId = getUserIdCallback(context.Identity);
                if ((object)manager == null || (object)userId == null)
                    return;
                TUser user = await manager.FindByIdAsync(userId);
                bool reject = true;
                if ((object)user != null && manager.SupportsUserSecurityStamp)
                {
                    string securityStamp = context.Identity.FindFirstValue("AspNet.Identity.SecurityStamp");
                    if (securityStamp == await manager.GetSecurityStampAsync(userId))
                    {
                        reject = false;
                        if (regenerateIdentityCallback != null)
                        {
                            ClaimsIdentity identity = await regenerateIdentityCallback(manager, user);
                            if (identity != null)
                            {
                                context.Properties.IssuedUtc = new DateTimeOffset?();
                                context.Properties.ExpiresUtc  = new DateTimeOffset?();
                                context.OwinContext.Authentication.SignIn(context.Properties, new ClaimsIdentity[1]
                                {
                                    identity
                                });
                            }
                        }
                    }
                }
                if (!reject)
                    return;
                context.RejectIdentity();
                context.OwinContext.Authentication.SignOut(new string[1]
                {
                    context.Options.AuthenticationType
                });
            });
        }

    }
}