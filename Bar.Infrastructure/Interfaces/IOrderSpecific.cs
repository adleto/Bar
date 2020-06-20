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
        Task Insert(List<ItemOrderInsertModel> list, int userId);
        Task<List<OrderModel>> GetToday();
        Task<List<OrderModel>> Get(int numberOf);
    }
}
