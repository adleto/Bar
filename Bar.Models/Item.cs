using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bar.Models
{
    public class Item
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Polje mora biti popunjeno.")]
        public string Naziv { get; set; }
        [Required(ErrorMessage = "Polje mora biti popunjeno.")]
        public decimal Price { get; set; }
        public int? ReferringToId { get; set; }
        public Item ReferringTo { get; set; }
        public List<Item> Vrste { get; set; }
    }
}
