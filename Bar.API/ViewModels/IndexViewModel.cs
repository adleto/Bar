using Bar.Database.Entities;
using Bar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bar.API.ViewModels
{
    public class IndexViewModel
    {
        public List<OrderModel> OrderList { get; set; }
    }
}
