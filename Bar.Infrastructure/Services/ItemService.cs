using AutoMapper;
using Bar.Database;
using Bar.Database.Entities;
using Bar.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bar.Infrastructure.Services
{
    public class ItemService : BaseCrudService<Item, Item, Item, Item, Item>
    {
        public ItemService(Context context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
