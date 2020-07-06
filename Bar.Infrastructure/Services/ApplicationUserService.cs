using Bar.Database;
using Bar.Database.Entities;
using Bar.Infrastructure.Interfaces;
using Bar.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bar.Infrastructure.Services
{
    public class ApplicationUserService : IApplicationUser
    {
        private readonly Context _context;
        private UserManager<ApplicationUser> _userManager;

        public ApplicationUserService(Context context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<UserViewModel> GetUser(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                var role = await _userManager.GetRolesAsync(user);
                return new UserViewModel
                {
                    Id = user.Id,
                    RoleNaziv = role[0],
                    Username = user.UserName
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<UserViewModel> GetUsers()
        {
            try
            {
                var users = _context.ApplicationUser
                .ToList();
                var returnModel = new List<UserViewModel>();
                users.ForEach(u => returnModel.Add(new UserViewModel
                {
                    Username = u.UserName,
                    Id = u.Id
                }));
                return returnModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UserAdd(UserViewModel model)
        {
            try
            {
                var result = await _userManager.CreateAsync(new ApplicationUser { UserName = model.Username }, model.Password);
                var user = await _userManager.FindByNameAsync(model.Username);
                await _userManager.AddToRoleAsync(user, model.RoleNaziv);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UserDelete(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                await _userManager.DeleteAsync(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UserEdit(UserViewModel model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                user.UserName = model.Username;
                await _userManager.ChangePasswordAsync(user, model.OldPassword, model.Password);
                var role = await _userManager.GetRolesAsync(user);
                if (!role.Contains(model.RoleNaziv))
                {
                    await _userManager.RemoveFromRolesAsync(user, role);
                    await _userManager.AddToRoleAsync(user, model.RoleNaziv);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
