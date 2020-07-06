using Bar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bar.API.ViewModels
{
    public class GetDataReportViewModel
    {
        public List<OrderModel> OrderList { get; set; }
        public List<ItemCounts> ItemCountsList { get; set; }
        public DateTime odDate { get; set; }
        public DateTime doDate { get; set; }
    }

    public class ItemCounts
    {
        public int ItemId { get; set; }
        public string Naziv { get; set; }
        public decimal TotalCijena { get; set; }
        public int TotalCount { get; set; }
    }
}
