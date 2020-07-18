using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bar.API.Helpers;
using Bar.API.Hubs;
using Bar.Database.Entities;
using Bar.Infrastructure.Interfaces;
using Bar.Infrastructure.Services;
using Bar.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Bar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = scheme)]
    public class OrderSpecificController : ControllerBase
    {
        private readonly IHubContext<MyHub> _hubContext;
        private readonly IOrderSpecific _orderSpecificService;
        private const string scheme = JwtBearerDefaults.AuthenticationScheme;
        public OrderSpecificController(IOrderSpecific orderSpecificService, IHubContext<MyHub> hubContext)
        {
            _orderSpecificService = orderSpecificService;
            _hubContext = hubContext;
        }
        [HttpPost]
        public async Task<IActionResult> Insert(OrderInsertModel model)
        {
            try
            {
                await _orderSpecificService.Insert(model, UserResolver.GetUserId(HttpContext.User));

                await _hubContext.Clients.All.SendAsync("RefreshMessage");

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
