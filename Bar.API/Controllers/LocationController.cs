using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bar.API.Controllers.Repository;
using Bar.Database.Entities;
using Bar.Infrastructure.Interfaces;
using Bar.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = scheme)]
    public class LocationController : ControllerBase
    {
        private readonly IDatabaseTimeStamp _databaseTimeStampService;
        private const string scheme = JwtBearerDefaults.AuthenticationScheme;
        private readonly ILocation _service;
        public LocationController(ILocation service, IDatabaseTimeStamp databaseTimeStampService)
        {
            _service = service;
            _databaseTimeStampService = databaseTimeStampService;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Location obj = null)
        {
            var result = await _service.Get(obj);
            return Ok(new Bar.Models.Locations.ApiGetLocationModel
            { 
                LocationList = result,
                TimeStamp = _databaseTimeStampService.Get()
            });
        }
    }
}
