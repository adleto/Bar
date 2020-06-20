using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bar.Infrastructure.Repository
{
    public interface IBaseCrudService<TModel, TSearch, TInsert, TUpdate> : IBaseService<TModel, TSearch>
    {
        Task<TModel> Update(int id, TUpdate obj);
        Task<TModel> Insert(TInsert obj);
        Task Delete(int id);
    }
}
