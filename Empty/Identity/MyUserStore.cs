using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;

namespace Empty.Identity
{
    public class MyUserStore : IUserStore<MyUser>, IUserPasswordStore<MyUser>
    {
        private static List<MyUser> _users;

        public List<MyUser> Users
        {
            get
            {
                if(_users == null)
                    _users = new List<MyUser> { new MyUser { Id = "1", Password = "1", UserName = "1"} };
                return _users;
            }
        }

        public void Dispose()
        {
        }

        public Task CreateAsync(MyUser user)
        {
            Users.Add(user);
            return Task.FromResult(0);
        }

        public Task UpdateAsync(MyUser user)
        {
            return Task.CompletedTask;
        }

        public Task DeleteAsync(MyUser user)
        {
            return Task.CompletedTask;
        }

        public Task<MyUser> FindByIdAsync(string userId)
        {
            return Task.FromResult(Users.SingleOrDefault(x => x.Id == userId));
        }

        public Task<MyUser> FindByNameAsync(string userName)
        {
            return Task.FromResult(Users.SingleOrDefault(x => x.UserName == userName));
        }

        public Task SetPasswordHashAsync(MyUser user, string passwordHash)
        {
            return Task.FromResult("123");
        }

        public Task<string> GetPasswordHashAsync(MyUser user)
        {
            return Task.FromResult("456");
        }

        public Task<bool> HasPasswordAsync(MyUser user)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.Password));
        }
    }
}