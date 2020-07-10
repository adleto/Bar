using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bar.API.ViewModels;
using Bar.Database.Entities;
using Bar.Infrastructure.Interfaces;
using Bar.Infrastructure.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bar.API.Controllers.WebControllers
{
    [Authorize]
    public class LocationController : Controller
    {
        private readonly ILocation _service;

        public LocationController(ILocation service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetLocationList()
        {
            try
            {
                List<Bar.Models.Location> list = await _service.Get(null);
                return PartialView("_LocationPartialListView", new LocationListViewModel
                {
                    LocationList = list
                });
            }
            catch
            {
                return BadRequest();
            }
        }
        public async Task<IActionResult> GetLocation(int id = 0)
        {
            try
            {
                var model = new Bar.Models.Location { Id = id };
                if (id != 0)
                {
                    model = await _service.Get(id);
                }
                return PartialView("_LocationPartialView", model);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> LocationAdd(Bar.Models.Location model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _service.Insert(model);
                    return Ok("Ok");
                }
                return PartialView("_LocationPartialView", model);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> LocationEdit(Bar.Models.Location model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _service.Update(model.Id, model);
                    return Ok("Ok");
                }
                return PartialView("_LocationPartialView", model);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> LocationDelete(int id)
        {
            try
            {
                await _service.ToggleActive(id);
                return Ok("Ok");
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
