using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace Empty.Identity
{
    public class MyUser : IUser
    {
        public string Id  { get; set; }
        public string UserName { get; set; }
    }
}