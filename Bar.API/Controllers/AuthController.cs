using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bar.Infrastructure.Interfaces;
using Bar.Infrastructure.Services;
using Bar.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _authService;

        public AuthController(IAuth authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("[action]")]
        //[Authorize(Roles="MasterUser")]
        public async Task<IActionResult> Register(ApplicationUserInsertModel model)
        {
            try
            {
                if(!ModelState.IsValid) throw new Exception();
                var result = await _authService.Register(model);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AuthTest(ApplicationUserGetRequestModel model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();
                var result = await _authService.Authenticate(model.Username, model.Password);
                if (result == null) return BadRequest();
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
