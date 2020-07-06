using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bar.Infrastructure.Interfaces;
using Bar.Models.Account;
using Bar.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Bar.API.Controllers.WebControllers
{
    [Authorize(Roles="MasterUser")]
    public class UserController : Controller
    {
        private readonly IApplicationUser _userService;

        public UserController(IApplicationUser userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetUserList()
        {
            try
            {
                List<UserViewModel> userList = _userService.GetUsers();
                return PartialView("_UserPartialListView", new UserListViewModel
                {
                    Users = userList
                });
            }
            catch
            {
                return BadRequest();
            }
        }
        public async Task<IActionResult> GetUser(string id = "0")
        {
            try
            {
                var model = new UserViewModel { Id = id };
                if (id != "0")
                {
                    model = await _userService.GetUser(id);
                }
                return PartialView("_UserPartialView", model);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> UserAdd(UserViewModel model)
        {
            try
            {
                if (model.RoleNaziv != "MasterUser" && model.RoleNaziv != "RegularUser")
                    ModelState.AddModelError(nameof(UserViewModel.RoleNaziv), "Neispravna uloga odabrana.");
                if (ModelState.IsValid)
                {
                    await _userService.UserAdd(model);
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
        public async Task<IActionResult> UserEdit(UserViewModel model)
        {
            try
            {
                if(string.IsNullOrEmpty(model.OldPassword))
                    ModelState.AddModelError(nameof(UserViewModel.OldPassword), "Trenutna lozinka mora biti unesena.");
                if (model.RoleNaziv != "MasterUser" && model.RoleNaziv != "RegularUser")
                    ModelState.AddModelError(nameof(UserViewModel.RoleNaziv), "Neispravna uloga odabrana.");
                if (ModelState.IsValid)
                {
                    await _userService.UserEdit(model);
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
        public async Task<IActionResult> UserDelete(string id)
        {
            try
            {
                await _userService.UserDelete(id);
                return Ok("Ok");
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
