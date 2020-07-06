using System;
using System.Threading.Tasks;
using Bar.Infrastructure.Interfaces;
using Bar.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _authService;
        private const string scheme = "BasicAuthentication";
        public AuthController(IAuth authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize(AuthenticationSchemes = scheme, Roles = "MasterUser")]
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
