using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Bar.API.Helpers;
using Bar.Database.Entities;
using Bar.Infrastructure.Interfaces;
using Bar.Models.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bar.API.Controllers.WebControllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [AnonymousOnly]
        public async Task<IActionResult> Login()
        {
            var model = new LoginVM
            {
                ErrorMessage = ""
            };
            if (!string.IsNullOrEmpty(model.ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, model.ErrorMessage);
            }
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            return View(model);
        }
        [AnonymousOnly]
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    return LocalRedirect("/Home/Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Pogrešan username ili lozinka.");
                    return View(model);
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return Redirect("/Home/");
            }
            await _signInManager.SignOutAsync();
            return Redirect("/Account/Login");
        }
    }
}
