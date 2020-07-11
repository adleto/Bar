using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bar.Models.Items
{
    public class ItemApiModel
    {
        [PrimaryKey]
        public int Id { get; set; }
        public int? ReferringToId { get; set; }
        public string Naziv { get; set; }
        public bool Active { get; set; } = true;
        public decimal Price { get; set; }
    }
}
