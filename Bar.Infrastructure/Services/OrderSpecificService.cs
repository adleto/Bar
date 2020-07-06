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
                ApplicationUser = r.ApplicationUser.UserName,
                DateTime = r.TimeOfOrder,
                Items = new List<ItemModel>(),
                Id = r.Id
            };
            foreach (var item in r.ItemOrderList)
            {
                var t = new ItemModel
                {
                    Id = item.ItemId,
                    Naziv = item.Item.Naziv,
                    Price = item.PojedinacnaCijena,
                    Count = item.Quantity
                };
                o.Items.Add(t);
            }
            return o;
        }

        public async Task<List<OrderModel>> Get(DateTime odDate, DateTime doDate, int take = 2000)
        {
            var result = await _context.Order
                .Include(o => o.ApplicationUser)
                .Include(o => o.ItemOrderList)
                    .ThenInclude(i => i.Item)
                .Where(o => o.TimeOfOrder>=odDate && o.TimeOfOrder<=doDate)
                .Take(take)
                .OrderByDescending(o => o.Id)
                .ToListAsync();
            var returnModel = new List<OrderModel>();
            result.ForEach(r => returnModel.Add(MapToOrderModel(r)));
            return returnModel;
        }

        public async Task Insert(List<ItemOrderInsertModel> list, string userId)
        {
            var order = new Order
            {
                TimeOfOrder = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Central European Standard Time"),
                ApplicationUserId = userId
            };
            _context.Add(order);
            var items = _context.Item.ToList();
            foreach (var i in list)
            {
                var itemCijena = items.Where(x => x.Id == i.ItemId).Select(x => x.Price).First();
                _context.Add(new ItemOrder { 
                    ItemId = i.ItemId,
                    Quantity = i.Quantity,
                    Order = order,
                    PojedinacnaCijena = itemCijena
                });
            }

            await _context.SaveChangesAsync();
        }
    }
}
