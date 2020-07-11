using System;
using System.Collections.Generic;
using System.Text;

namespace Bar.Models.Items
{
    public class ApiGetItemModel
    {
        public DateTime TimeStamp { get; set; }
        public List<ItemApiModel> ItemList { get; set; }
    }
}
