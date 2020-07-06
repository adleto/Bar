using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bar.API.Helpers;
using Bar.Database.Entities;
using Bar.Infrastructure.Interfaces;
using Bar.Infrastructure.Services;
using Bar.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = scheme)]
    public class OrderSpecificController : ControllerBase
    {
        private readonly IOrderSpecific _orderSpecificService;
        private const string scheme = "BasicAuthentication";
        public OrderSpecificController(IOrderSpecific orderSpecificService)
        {
            _orderSpecificService = orderSpecificService;
        }
        [HttpPost]
        public async Task<IActionResult> Insert(List<ItemOrderInsertModel> list)
        {
            try
            {
                await _orderSpecificService.Insert(list, UserResolver.GetUserId(HttpContext.User));
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
