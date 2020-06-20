using AutoMapper;
using Bar.Database;
using Bar.Database.Entities;
using Bar.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bar.Infrastructure.Services
{
    public class OrderService : BaseCrudService<Order, Order, Order, Order, Order>
    {
        public OrderService(Context context, IMapper mapper) : base(context, mapper)
        {
        }
        public override async Task<List<Order>> Get(Order search = null)
        {
            //var query = _context.Set<Order>().AsQueryable();
            //_context.Set<Order>().Include(o => o.)
            //if (search != null && !string.IsNullOrEmpty(search.Name))
            //{
            //    query = query.Where(x => x.Name.StartsWith(search.Name));
            //}
            //return _mapper.Map<List<ItemGetModel>>(await query.ToListAsync());
            return await _context.Order
                .Include(o => o.ItemOrderList)
                    .ThenInclude(io => io.Item)
                .OrderByDescending(io => io.TimeOfOrder)
                .ToListAsync();
        }
    }
}
