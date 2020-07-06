using System;
using System.Collections.Generic;
using System.Text;

namespace Bar.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public List<ItemModel> Items { get; set; }
        public string ApplicationUser { get; set; }
    }
}
