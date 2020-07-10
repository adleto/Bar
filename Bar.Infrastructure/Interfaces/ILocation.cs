using Bar.Database.Entities;
using Bar.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bar.Infrastructure.Interfaces
{
    public interface ILocation : IBaseCrudService<Bar.Models.Location, Location, Bar.Models.Location, Bar.Models.Location>
    {
        new Task<List<Bar.Models.Location>> Get(Location obj = null);
        Task ToggleActive(int id);
    }
}
