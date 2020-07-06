using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bar.Models.Account
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage ="Username mora biti unesen.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password mora biti unesen.")]
        [MinLength(4, ErrorMessage = "Password mora sadržati najmanje 4 karaktera.")]
        public string Password { get; set; }
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Uloga mora biti odabrana.")]
        public string RoleNaziv { get; set; }
    }
}
