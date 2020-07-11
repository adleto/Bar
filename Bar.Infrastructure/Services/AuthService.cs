using Bar.Database;
using Bar.Database.Entities;
using Bar.Infrastructure.Helpers;
using Bar.Infrastructure.Interfaces;
using Bar.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Bar.Infrastructure.Services
{
    public class AuthService : IAuth
    {
        private readonly Context _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public AuthService(Context context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<ApplicationUserModel> Authenticate(string username, string password)
        {
            var user = await _context.ApplicationUser
                .FirstOrDefaultAsync(x => x.UserName == username);
            if (user != null)
            {
                if (await _userManager.CheckPasswordAsync(user, password))
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var model = new ApplicationUserModel
                    {
                        Id = user.Id,
                        Username = user.UserName,
                        Roles = new List<RoleModel>()
                    };
                    foreach (var role in roles)
                    {
                        model.Roles.Add(new RoleModel
                        {
                            Naziv = role
                        });
                    }
                    return model;
                }
            }
            return null;
        }

        public async Task<ApplicationUserModel> Register(ApplicationUserInsertModel model)
        {
            try
            {
                if (!(IsUsernameUnique(model.Username)))
                {
                    throw new Exception("Username je već zauzet.");
                }
                var user = new ApplicationUser
                {
                    UserName = model.Username
                };
                await _userManager.CreateAsync(user, model.Password);
                await _userManager.AddToRoleAsync(user, model.RoleNaziv);
                var created = new ApplicationUserModel
                {
                    Username = user.UserName
                };
                return created;
            }
            catch
            {
                throw;
            }
        }

        private bool IsUsernameUnique(string username)
        {
            var user = _context.ApplicationUser
                .Where(u => u.UserName == username)
                .FirstOrDefault();
            if (user == null) return true;
            return false;
        }
    }
}
