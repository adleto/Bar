using System;
using System.Collections.Generic;
using System.Text;

namespace Bar.Models.Order
{
    public class MobileOrderModel
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public virtual List<MobileOrderListingItemModel> Items { get; set; }
    }
    public class MobileOrderListingItemModel
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int MobileOrderId { get; set; }
        //public string Naziv { get; set; }
        public int Kolicina { get; set; }
        public string DodatniOpis { get; set; }
    }
}
