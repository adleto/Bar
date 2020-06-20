using System;
using System.Collections.Generic;
using System.Text;

namespace Bar.Models
{
    public class ApplicationUserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public RoleModel Role { get; set; }
    }
}
