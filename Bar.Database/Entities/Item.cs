using System;
using System.Collections.Generic;
using System.Text;

namespace Bar.Database.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        //public bool Active { get; set; }
        public decimal Price { get; set; }
    }
}
