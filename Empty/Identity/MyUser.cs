using Microsoft.AspNet.Identity;

namespace Empty.Identity
{
    public class MyUser : IUser
    {
        public string Id  { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}