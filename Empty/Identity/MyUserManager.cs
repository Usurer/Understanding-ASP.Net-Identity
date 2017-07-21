using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;

namespace Empty.Identity
{
    public class MyUserManager : UserManager<MyUser>
    {
        public MyUserManager(IUserStore<MyUser> store) : base(store)
        {
        }

        public override async Task<IdentityResult> CreateAsync(MyUser user)
        {
            await Store.CreateAsync(user);
            return IdentityResult.Success;
        }

        public override async Task<IdentityResult> CreateAsync(MyUser user, string password)
        {
            await Store.CreateAsync(user);
            return IdentityResult.Success;
        }
    }
}