using AutoMapper;
using Bar.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bar.Infrastructure.Repository
{
    public class BaseCrudService<TModel, TSearch, TEntity, TInsert, TUpdate> : 
        BaseService<TModel, TSearch, TEntity>, 
        IBaseCrudService<TModel, TSearch, TInsert, TUpdate>
            where TEntity : class
    {
        public BaseCrudService(Context context, IMapper mapper) : base(context, mapper) {}

        public virtual async Task Delete(int id)
        {
            try {
            var obj = _context.Set<TEntity>().Find(id);
            _context.Remove(obj);
            await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public virtual async Task<TModel> Insert(TInsert obj)
        {
            var entity = _mapper.Map<TEntity>(obj);
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<TModel>(entity);
        }

        public virtual async Task<TModel> Update(int id, TUpdate obj)
        {
            var entity = _mapper.Map<TEntity>(obj);
            _context.Set<TEntity>().Attach(entity);
            _context.Set<TEntity>().Update(entity);

            _mapper.Map(obj, entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return _mapper.Map<TModel>(entity);
        }
    }
}
