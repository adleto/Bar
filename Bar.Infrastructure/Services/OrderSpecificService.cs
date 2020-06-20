using Bar.Database;
using Bar.Database.Entities;
using Bar.Infrastructure.Interfaces;
using Bar.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bar.Infrastructure.Services
{
    public class OrderSpecificService : IOrderSpecific
    {
        private readonly Context _context;

        public OrderSpecificService(Context context)
        {
            _context = context;
        }

        public async Task<List<OrderModel>> Get(int numberOf)
        {
            var result = await _context.Order
                .Include(o => o.ApplicationUser)
                .Include(o => o.ItemOrderList)
                    .ThenInclude(i => i.Item)
                .OrderByDescending(o => o.Id)
                .Take(numberOf)
                .ToListAsync();
            var returnModel = new List<OrderModel>();
            result.ForEach(r => returnModel.Add(MapToOrderModel(r)));
            return returnModel;
        }

        private OrderModel MapToOrderModel(Order r)
        {
            var o = new OrderModel
            {
                ApplicationUser = r.ApplicationUser.Username,
                DateTime = r.TimeOfOrder,
                Items = new List<Tuple<ItemModel, int>>(),
                Id = r.Id
            };
            foreach (var item in r.ItemOrderList)
            {
                var t = new Tuple<ItemModel, int>(new ItemModel
                {
                    Id = item.ItemId,
                    Naziv = item.Item.Naziv,
                    Price = item.Item.Price
                }, item.Quantity);
                o.Items.Add(t);
            }
            return o;
        }

        public async Task<List<OrderModel>> GetToday()
        {
            var result = await _context.Order
                .Include(o => o.ApplicationUser)
                .Include(o => o.ItemOrderList)
                    .ThenInclude(i => i.Item)
                .Where(o => o.TimeOfOrder.Date == DateTime.Now.Date)
                .OrderByDescending(o => o.Id)
                .ToListAsync();
            var returnModel = new List<OrderModel>();
            result.ForEach(r => returnModel.Add(MapToOrderModel(r)));
            return returnModel;
        }

        public async Task Insert(List<ItemOrderInsertModel> list, int userId)
        {
            var order = new Order
            {
                TimeOfOrder = DateTime.Now,
                ApplicationUserId = userId
            };
            _context.Add(order);

            foreach (var i in list)
            {
                _context.Add(new ItemOrder { 
                    ItemId = i.ItemId,
                    Quantity = i.Quantity,
                    Order = order
                });
            }

            await _context.SaveChangesAsync();
        }
    }
}
