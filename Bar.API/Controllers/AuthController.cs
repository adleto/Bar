using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Bar.Infrastructure.Interfaces;
using Bar.Models;
using Bar.Models.Account;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Bar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _authService;
        private const string scheme = JwtBearerDefaults.AuthenticationScheme;
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
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes("P696m]A=wowk3{=Rwwgeg34gg42aIHL^ou_U:1]tf7ZT'aigae42ej2Fp=sz/@fMe1TK");
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, result.Username),
                    new Claim("UserId", result.Id)
                };
                var token = new JwtSecurityToken(
                    expires: DateTime.Now.AddMonths(1),
                    claims: claims,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                );
                return Ok(new TokenModel { Token = tokenHandler.WriteToken(token) });
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
