using Bar.Database.Entities;
using Bar.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bar.Infrastructure.Interfaces
{
    public interface IOrderSpecific
    {
        Task Insert(OrderInsertModel model, string userId);
        Task<List<OrderModel>> Get(DateTime odDate, DateTime doDate, int take = 2000);
        Task<List<OrderModel>> Get(int numberOf);
        Task ToggleActivity(int id, string userId);
        OrderModel GetById(int id);
        Task<List<OrderModel>> GetMijenjanoStanje(int takeDays = 30);
    }
}
