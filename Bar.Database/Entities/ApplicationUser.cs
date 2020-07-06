using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bar.Database.Entities
{
    public class ApplicationUser:IdentityUser
    {
        //public int Id { get; set; }
        //public string Username { get; set; }
        //public string PasswordSalt { get; set; }
        //public string PasswordHash { get; set; }
        //public virtual Role Role { get; set; }
        //[ForeignKey(nameof(Role))]
        //public int RoleId { get; set; }
    }
}
