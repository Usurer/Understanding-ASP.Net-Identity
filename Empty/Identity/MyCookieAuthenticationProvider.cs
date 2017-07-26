using System;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Cookies;

namespace Empty.Identity
{
    public class MyCookieAuthenticationProvider : ICookieAuthenticationProvider
    {
        public Func<CookieValidateIdentityContext, Task> OnValidateIdentity { get; set; }

        public Task ValidateIdentity(CookieValidateIdentityContext context)
        {
            return OnValidateIdentity(context);
        }

        public void ResponseSignIn(CookieResponseSignInContext context)
        {
        }

        public void ApplyRedirect(CookieApplyRedirectContext context)
        {
        }
    }
}