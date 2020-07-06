using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bar.Models.Account
{
    public class LoginVM
    {
        public string ErrorMessage { get; set; }
        [Required(ErrorMessage = "Korisničko ime mora biti uneseno.")]
        [Display(Name = "Korisničko ime")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Lozinka mora biti unesena.")]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }
    }
}
