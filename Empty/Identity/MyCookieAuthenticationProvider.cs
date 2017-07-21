using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin;
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
            throw new NotImplementedException();
        }

        public void ApplyRedirect(CookieApplyRedirectContext context)
        {
            throw new NotImplementedException();
        }
    }
}