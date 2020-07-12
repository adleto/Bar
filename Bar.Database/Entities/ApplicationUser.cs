using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bar.Database.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public bool Active { get; set; }
    }
}
