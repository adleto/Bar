using System;
using System.Collections.Generic;
using System.Text;

namespace Bar.Models
{
    public class OrderInsertModel
    {
        public List<ItemOrderInsertModel> List { get; set; }
        public int? LocationId { get; set; }
    }
}
