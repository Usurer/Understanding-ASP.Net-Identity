using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;

namespace Empty.Identity
{
    public class MyPasswordValidator : IIdentityValidator<string>
    {
        public Task<IdentityResult> ValidateAsync(string item)
        {
            return Task.FromResult(IdentityResult.Success);
        }
    }
}