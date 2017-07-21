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
            this.PasswordValidator = new PasswordValidator();
        }

        //public override async Task<IdentityResult> CreateAsync(MyUser user)
        //{
        //    await Store.CreateAsync(user);
        //    return IdentityResult.Success;
        //}

        public override async Task<IdentityResult> CreateAsync(MyUser user, string password)
        {
            user.Password = password;
            return await base.CreateAsync(user, password);
        }
    }
}