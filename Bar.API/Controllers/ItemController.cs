using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bar.API.Controllers.Repository;
using Bar.Database.Entities;
using Bar.Infrastructure.Interfaces;
using Bar.Infrastructure.Repository;
using Bar.Models.Items;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = scheme)]
    public class ItemController : ControllerBase/*: BaseCrudController<Item, Item, Item, Item>*/
    {
        private readonly IDatabaseTimeStamp _databaseTimeStampService;
        private readonly IItem _service;
        private const string scheme = JwtBearerDefaults.AuthenticationScheme;
        private readonly IMapper _mapper;
        public ItemController(IItem service, IDatabaseTimeStamp databaseTimeStampService, IMapper mapper)/* : base(service)*/
        {
            _service = service;
            _databaseTimeStampService = databaseTimeStampService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Item obj = null)
        {
            var result = await _service.Get(obj);
            return Ok(new ApiGetItemModel { 
                ItemList = _mapper.Map<List<ItemApiModel>>(result),
                TimeStamp = _databaseTimeStampService.Get()
            });
        }
    }
}