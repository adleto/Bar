using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bar.Database.Entities;
using Bar.Infrastructure.Repository;
using Bar.Models.Items;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bar.API.Controllers.WebControllers
{
    [Authorize]
    public class ItemController : Controller
    {
        private readonly IBaseCrudService<Item, Item, Item, Item> _itemService;
        private readonly IMapper _mapper;

        public ItemController(IBaseCrudService<Item, Item, Item, Item> itemService, IMapper mapper)
        {
            _itemService = itemService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetItemList()
        {
            try
            {
                List<Item> itemList = await _itemService.Get(null);
                return PartialView("_ItemPartialListView", new ItemListViewModel
                {
                    Items = _mapper.Map<List<Bar.Models.Item>>(itemList)
                });
            }
            catch
            {
                return BadRequest();
            }
        }
        public async Task<IActionResult> GetItem(int id = 0)
        {
            try
            {
                var model = new Item { Id = id };
                if (id != 0)
                {
                    model = await _itemService.Get(id);
                }
                return PartialView("_ItemPartialView", _mapper.Map<Bar.Models.Item>(model));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> ItemAdd(Bar.Models.Item model)
        {
            try
            {
                if (model.Price <= 0) ModelState.AddModelError("Price", "Cijena mora biti veća od 0");
                if (ModelState.IsValid)
                {
                    await _itemService.Insert(_mapper.Map<Item>(model));
                    return Ok("Ok");
                }
                return PartialView("_ItemPartialView", model);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> ItemEdit(Bar.Models.Item model)
        {
            try
            {
                if (model.Price <= 0) ModelState.AddModelError("Price", "Cijena mora biti veća od 0");
                if (ModelState.IsValid)
                {
                    await _itemService.Update(model.Id, _mapper.Map<Item>(model));
                    return Ok("Ok");
                }
                return PartialView("_UserPartialView", model);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> ItemDelete(int id)
        {
            try
            {
                await _itemService.Delete(id);
                return Ok("Ok");
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
