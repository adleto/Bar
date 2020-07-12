using System;
using System.Collections.Generic;
using System.Text;

namespace Bar.Models.Order
{
    public class MobileOrderModel
    {
        public List<MobileOrderListingItemModel> OrderList { get; set; }
        public string Lokacija { get; set; }
    }
    public class MobileOrderListingItemModel
    {
        public string Naziv { get; set; }
        public int Kolicina { get; set; }
        public string DodatniOpis { get; set; }
    }
}
