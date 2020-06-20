using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bar.Infrastructure.Repository
{
    public interface IBaseService<T, TSearch>
    {
        Task<List<T>> Get(TSearch obj);
        Task<T> Get(int id);
    }
}
