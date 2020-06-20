using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bar.Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bar.API.Controllers.Repository
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseCrudController<TModel, TSearch, TInsert, TUpdate> : BaseController<TModel, TSearch>
    {
        private readonly IBaseCrudService<TModel, TSearch, TInsert, TUpdate> _service;

        public BaseCrudController(IBaseCrudService<TModel, TSearch, TInsert, TUpdate> service)
            : base(service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<TModel> Insert(TInsert obj)
        {
            return await _service.Insert(obj);
        }
        [HttpPut("{id}")]
        public async Task<TModel> Update(int id, [FromBody]TUpdate obj)
        {
            return await _service.Update(id, obj);
        }
    }
}