using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bar.API.ViewModels;
using Bar.Database.Entities;
using Bar.Infrastructure.Interfaces;

namespace Bar.API.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOrderSpecific _orderService;
        public HomeController(ILogger<HomeController> logger, IOrderSpecific orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            return View(new IndexViewModel
            {
                OrderList = await _orderService.Get(5)
            });
        }
        public async Task<IActionResult> Today()
        {
            return View("Index", new IndexViewModel
            {
                OrderList = await _orderService.GetToday()
            });
        }
    }
}
