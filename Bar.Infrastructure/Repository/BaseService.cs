using AutoMapper;
using Bar.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bar.Infrastructure.Repository
{
    public class BaseService<TModel, TSearch, TEntity> : IBaseService<TModel, TSearch>
        where TEntity : class
    {
        protected readonly Context _context;
        protected readonly IMapper _mapper;

        public BaseService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public virtual async Task<List<TModel>> Get(TSearch obj)
        {
            return _mapper.Map<List<TModel>>(await _context.Set<TEntity>().ToListAsync());
        }

        public virtual async Task<TModel> Get(int id)
        {
            return _mapper.Map<TModel>(await _context.Set<TEntity>().FindAsync(id));
        }
        
    }
}
