using Bar.Database;
using Bar.Database.Entities;
using Bar.Infrastructure.Helpers;
using Bar.Infrastructure.Interfaces;
using Bar.Models;
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

        public AuthService(Context context)
        {
            _context = context;
        }

        public async Task<ApplicationUserModel> Authenticate(string username, string password)
        {
            var user = await _context.ApplicationUser
                .Include(au => au.Role)
                .FirstOrDefaultAsync(x => x.Username == username);
            if (user != null)
            {
                if (user.PasswordHash == AuthHelper.GenerateHash(user.PasswordSalt, password))
                {
                    var roleModel = new RoleModel
                    {
                        Id = user.Role.Id,
                        Naziv = user.Role.Name
                    };
                    var model = new ApplicationUserModel
                    {
                        Id = user.Id,
                        Username = user.Username,
                        Role = roleModel
                    };
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
                    PasswordSalt = AuthHelper.GenerateSalt(),
                    Username = model.Username,
                    RoleId = model.RoleId
                };
                user.PasswordHash = AuthHelper.GenerateHash(user.PasswordSalt, model.Password);
                _context.ApplicationUser.Add(user);
                await _context.SaveChangesAsync();
                var created = new ApplicationUserModel
                {
                    Username = user.Username,
                    Id = user.Id
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
                .Where(u => u.Username == username)
                .FirstOrDefault();
            if (user == null) return true;
            return false;
        }
    }
}
