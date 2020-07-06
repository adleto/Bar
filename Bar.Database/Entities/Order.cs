using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bar.Database.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime TimeOfOrder { get; set; }
        public virtual List<ItemOrder> ItemOrderList { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserId { get; set; }
    }
}
