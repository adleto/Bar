using AutoMapper;
using Bar.Database;
using Bar.Database.Entities;
using Bar.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bar.Infrastructure.Services
{
    public class ItemOrderService : BaseCrudService<ItemOrder, ItemOrder, ItemOrder, ItemOrder, ItemOrder>
    {
        public ItemOrderService(Context context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
