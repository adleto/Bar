using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bar.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bar.API.Controllers.WebControllers
{
    [Authorize(Roles ="MasterUser")]
    public class OrderController : Controller
    {
        private readonly IOrderSpecific _orderSpecificService;
        public OrderController(IOrderSpecific orderSpecificService)
        {
            _orderSpecificService = orderSpecificService;
        }
        public async Task<IActionResult> MijenjanoStanje()
        {
            return View(await _orderSpecificService.GetMijenjanoStanje(30));
        }
    }
}
