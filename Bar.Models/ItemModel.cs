using System;
using System.Collections.Generic;
using System.Text;

namespace Bar.Models
{
    public class ItemModel
    {
        public int Id { get; set; }
        public int? ReferringToId { get; set; }
        public string ReferringToNaziv { get; set; }
        public string Naziv { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public string DodatniOpis { get; set; }
    }
}
