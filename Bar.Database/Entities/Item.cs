using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bar.Database.Entities
{
    public class Item
    {
        public int Id { get; set; }
        [ForeignKey(nameof(ReferringTo))]
        public int? ReferringToId { get; set; }
        public virtual Item ReferringTo { get; set; }
        public string Naziv { get; set; }
        public bool Active { get; set; } = true;
        public decimal Price { get; set; }
    }
}
