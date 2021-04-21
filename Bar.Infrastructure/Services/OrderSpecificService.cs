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
        public OrderModel GetById(int id)
        {
            var result = _context.Order
                .Include(o => o.ApplicationUser)
                .Include(o => o.Location)
                .Include(o => o.ItemOrderList)
                    .ThenInclude(i => i.Item)
                .Where(o => o.Id == id)
                .First();
            return MapToOrderModel(result);
        }

        public async Task<List<OrderModel>> Get(int numberOf)
        {
            var result = await _context.Order
                .Include(o => o.ApplicationUser)
                .Include(o => o.Location)
                .Include(o => o.ItemOrderList)
                    .ThenInclude(i => i.Item)
                .Where(o => o.Active == true)
                .Take(numberOf)
                .OrderByDescending(o => o.Id)
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
                Id = r.Id,
                Active = r.Active
            };
            if (r.LastChangeMadeBy != null) o.ModifiedBy = r.LastChangeMadeBy.UserName;
            if (r.Location != null) o.Location = r.Location.Description;
            else o.Location = "";
            foreach (var item in r.ItemOrderList)
            {
                var t = new ItemModel
                {
                    Id = item.ItemId,
                    Naziv = item.Item.Naziv,
                    Price = item.PojedinacnaCijena,
                    Count = item.Quantity,
                    ReferringToId = item.Item.ReferringToId,
                    DodatniOpis = item.DodatniOpis
                };
                if (item.Item.ReferringTo != null) t.ReferringToNaziv = item.Item.ReferringTo.Naziv;
                o.Items.Add(t);
            }
            return o;
        }

        public async Task<List<OrderModel>> Get(DateTime odDate, DateTime doDate, int take = 2000)
        {
            var result = await _context.Order
                .Include(o => o.ApplicationUser)
                .Include(o => o.Location)
                .Include(o => o.ItemOrderList)
                    .ThenInclude(i => i.Item)
                        .ThenInclude(it => it.ReferringTo)
                .Where(o => o.Active == true && o.TimeOfOrder>=odDate && o.TimeOfOrder<=doDate)
                .Take(take)
                .OrderByDescending(o => o.Id)
                .ToListAsync();
            var returnModel = new List<OrderModel>();
            result.ForEach(r => returnModel.Add(MapToOrderModel(r)));
            return returnModel;
        }

        public async Task Insert(OrderInsertModel model, string userId)
        {
            DateTime now = DateTime.Now;
            //try
            //{
            //    now = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, ("Central European Standard Time"));
            //}
            //catch (TimeZoneNotFoundException)
            //{
            //    now = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Europe/Belgrade");
            //}
            var order = new Order
            {
                TimeOfOrder = now,
                ApplicationUserId = userId,
                Active = true,
                LocationId = model.LocationId
            };
            _context.Add(order);
            var items = _context.Item.ToList();
            foreach (var i in model.List)
            {
                var itemCijena = items.Where(x => x.Id == i.ItemId).Select(x => x.Price).First();
                var insertModel = new ItemOrder
                {
                    ItemId = i.ItemId,
                    Quantity = i.Quantity,
                    Order = order,
                    PojedinacnaCijena = itemCijena
                };
                if (!string.IsNullOrEmpty(i.DodatniOpis)) insertModel.DodatniOpis = i.DodatniOpis;
                _context.Add(insertModel);
            }

            await _context.SaveChangesAsync();
        }

        public async Task ToggleActivity(int id, string userId)
        {
            var order = _context.Order.Find(id);
            if (DateTime.Now.AddDays(-2) > order.TimeOfOrder) return;
            if (order.Active) order.Active = false;
            else order.Active = true;
            order.LastChangeMadeById = userId;
            await _context.SaveChangesAsync();
        }

        public async Task<List<OrderModel>> GetMijenjanoStanje(int takeDays = 30)
        {
            var result = await _context.Order
                .Include(o => o.ApplicationUser)
                .Include(o => o.LastChangeMadeBy)
                .Include(o => o.ItemOrderList)
                    .ThenInclude(i => i.Item)
                .Where(o => o.LastChangeMadeBy != null && DateTime.Now.AddDays(-30) <= o.TimeOfOrder)
                .OrderByDescending(o => o.Id)
                .ToListAsync();
            var returnModel = new List<OrderModel>();
            result.ForEach(r => returnModel.Add(MapToOrderModel(r)));
            return returnModel;
        }
    }
}
