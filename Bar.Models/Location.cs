using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bar.Models
{
    public class Location
    {
        [PrimaryKey]
        public int Id { get; set; }
        [Required(ErrorMessage = "Opis mora biti unesen.")]
        public string Description { get; set; }
        public string Color { get; set; }
        public bool Active { get; set; } = true;
    }
}
