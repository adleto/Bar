using System;
using System.Collections.Generic;
using System.Text;

namespace Bar.Models
{
    public class ApplicationUserModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public List<RoleModel> Roles { get; set; }
    }
}
