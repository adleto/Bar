using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bar.Infrastructure.Interfaces;
using Bar.Models.DatabaseTimeStamp;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = scheme)]
    public class DatabaseTimeStampController : ControllerBase
    {
        private readonly IDatabaseTimeStamp _databaseTimeStampService;
        private const string scheme = JwtBearerDefaults.AuthenticationScheme;

        public DatabaseTimeStampController(IDatabaseTimeStamp databaseTimeStampService)
        {
            _databaseTimeStampService = databaseTimeStampService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(new TimeStampModel{
                    TimeStamp = _databaseTimeStampService.Get() 
                });
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
