using AutoMapper;
using Bar.Database;
using Bar.Database.Entities;
using Bar.Infrastructure.Interfaces;
using Bar.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bar.Infrastructure.Services
{
    public class ItemService : BaseCrudService<Item, Item, Item, Item, Item>, IItem
    {
        public ItemService(Context context, IMapper mapper) : base(context, mapper)
        {}
        public override async Task<List<Item>> Get(Item obj = null)
        {
            return await _context.Item
                .Include(i => i.ReferringTo)
                .Where(i => i.Active == true)
                .ToListAsync();
        }
        public override async Task<Item> Insert(Item model)
        {
            _context.DatabaseTimeStamp.First().TimeStamp = DateTime.Now;
            _context.Item.Add(model);
            if (model.ReferringToId == 0) model.ReferringToId = null;
            if (model.ReferringToId != null)
            {
                var original = _context.Item.Find(model.ReferringToId);
                model.Price = original.Price;
            }
            await _context.SaveChangesAsync();
            return model;
        }
        public override async Task<Item> Update(int id, Item model)
        {
            _context.DatabaseTimeStamp.First().TimeStamp = DateTime.Now;
            var entity = _context.Item.Find(id);
            entity.Naziv = model.Naziv;
            entity.Price = model.Price;
            if (model.ReferringToId == 0)
            {
                entity.ReferringToId = null;
            }
            else
            {
                entity.ReferringToId = model.ReferringToId;
            }
            if(entity.ReferringToId != null)
            {
                var original = _context.Item.Find(model.ReferringToId);
                entity.Price = original.Price;
                entity.ReferringTo = original;
            }
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task ToggleActive(int id)
        {
            _context.DatabaseTimeStamp.First().TimeStamp = DateTime.Now;
            var item = _context.Item.Find(id);
            if (item.Active) item.Active = false;
            else item.Active = true;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Item>> GetVrste()
        {
            return await _context.Item
                .Where(i => i.ReferringToId == null && i.Active == true)
                .ToListAsync();
        }
    }
}
