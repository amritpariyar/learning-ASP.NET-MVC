using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOIT.Data.ViewModels
{
    public class UserWithRolesViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string  Email { get; set; }
        public string RoleNames { get; set; }
    }
}