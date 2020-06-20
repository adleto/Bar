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
    public abstract class BaseController<TModel, TSearch> : ControllerBase
    {
        private readonly IBaseService<TModel, TSearch> _service;

        protected BaseController(IBaseService<TModel, TSearch> service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<List<TModel>> Get([FromQuery] TSearch obj)
        {

            var result = await _service.Get(obj);
            return result;
        }
        [HttpGet("{id}")]
        public async Task<TModel> Get(int id)
        {
            return await _service.Get(id);
        }
    }
}