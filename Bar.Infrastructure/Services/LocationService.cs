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
    public class LocationService : BaseCrudService<Bar.Models.Location, Database.Entities.Location, Database.Entities.Location, Bar.Models.Location, Bar.Models.Location>, ILocation
    {
        public LocationService(Context context, IMapper mapper) : base(context, mapper)
        {}
        public override Task<Models.Location> Insert(Models.Location obj)
        {
            _context.DatabaseTimeStamp.First().TimeStamp = DateTime.Now;
            return base.Insert(obj);
        }
        public override Task<Models.Location> Update(int id, Models.Location obj)
        {
            _context.DatabaseTimeStamp.First().TimeStamp = DateTime.Now;
            return base.Update(id, obj);
        }

        public override async Task<List<Bar.Models.Location>> Get(Database.Entities.Location obj = null)
        {
            return _mapper.Map<List<Bar.Models.Location>>(await _context.Location.Where(i => i.Active == true).ToListAsync());
        }
        public async Task ToggleActive(int id)
        {
            _context.DatabaseTimeStamp.First().TimeStamp = DateTime.Now;
            var item = _context.Location.Find(id);
            if (item.Active) item.Active = false;
            else item.Active = true;
            await _context.SaveChangesAsync();
        }
    }
}
