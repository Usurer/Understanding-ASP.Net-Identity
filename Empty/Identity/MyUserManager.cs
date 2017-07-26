using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Empty.Identity
{
    public class MyUserManager : Microsoft.AspNet.Identity.UserManager<MyUser>
    {
        public MyUserManager(IUserStore<MyUser> store) : base(store)
        {
            this.PasswordValidator = new PasswordValidator();
        }

        public override async Task<IdentityResult> CreateAsync(MyUser user, string password)
        {
            user.Password = password;
            return await base.CreateAsync(user, password);
        }
    }
}