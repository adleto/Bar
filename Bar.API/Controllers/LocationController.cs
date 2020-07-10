using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bar.API.Controllers.Repository;
using Bar.Database.Entities;
using Bar.Infrastructure.Interfaces;
using Bar.Infrastructure.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = scheme)]
    public class LocationController : BaseCrudController<Bar.Models.Location, Location, Bar.Models.Location, Bar.Models.Location>
    {
        private const string scheme = "BasicAuthentication";
        private readonly ILocation _service;
        public LocationController(ILocation service) : base(service)
        {
            _service = service;
        }
        [HttpGet]
        public override async Task<List<Bar.Models.Location>> Get([FromQuery] Location obj = null)
        {
            var result = await _service.Get(obj);
            return result;
        }
    }
}
