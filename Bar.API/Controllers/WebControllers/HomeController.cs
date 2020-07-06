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
using Microsoft.AspNetCore.Authorization;
using Bar.Models;

namespace Bar.API.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IOrderSpecific _orderService;
        public HomeController(IOrderSpecific orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetData(int take = 5)
        {
            try
            {
                return PartialView("_DataPartialListView", new IndexViewModel
                {
                    OrderList = await _orderService.Get(take)
                });
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public async Task<IActionResult> Report(DateTime? odDate = null, DateTime? doDate = null, int take = 2000)
        {
            try
            {
                DateTime odD;
                DateTime doD;
                try
                {
                    odD = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, ("Central European Standard Time"));
                    doD = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, ("Central European Standard Time"));
                }
                catch (TimeZoneNotFoundException)
                {
                    odD = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Europe/Belgrade");
                    doD = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Europe/Belgrade");
                }
                if (odDate == null || doDate == null) return View(new GetDataReportViewModel { 
                    ItemCountsList = new List<ItemCounts>(),
                    OrderList = new List<OrderModel>(),
                    doDate = doD,
                    odDate = odD
                });
                var orderList = await _orderService.Get((DateTime)odDate, (DateTime)doDate, take);
                return View(new GetDataReportViewModel
                {
                    OrderList = orderList,
                    ItemCountsList = CountItems(orderList),
                    odDate = (DateTime)odDate,
                    doDate = (DateTime)doDate
                });
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id, string returnUrl = "/Home/")
        {
            try
            {
                await _orderService.RemoveOrder(id);
                return Redirect(returnUrl);
            }
            catch
            {
                return BadRequest();
            }
        }
        private List<ItemCounts> CountItems(List<OrderModel> orderList)
        {
            var list = new List<ItemCounts>();
            var containedId = new List<int>();
            foreach(var order in orderList)
            {
                foreach(var listing in order.Items)
                {
                    if (!containedId.Contains(listing.Id))
                    {
                        containedId.Add(listing.Id);
                        list.Add(new ItemCounts
                        {
                            Naziv = listing.Naziv,
                            TotalCijena = listing.Price * listing.Count,
                            TotalCount = listing.Count,
                            ItemId = listing.Id
                        });
                    }
                    else
                    {
                        var entry = list.Where(x => x.ItemId == listing.Id).First();
                        entry.TotalCijena += listing.Price * listing.Count;
                        entry.TotalCount += listing.Count;
                    }
                }
            }
            return list;
        }
    }
}
