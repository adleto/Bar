using Bar.Database.Entities;
using Bar.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Bar.Infrastructure.Interfaces
{
    public interface IItem : IBaseCrudService<Item, Item, Item, Item>
    {
        new Task<List<Item>> Get(Item obj = null);
        Task ToggleActive(int id);
        Task<List<Item>> GetVrste();
        Task<List<Item>> GetDeletedItems();
    }
}
