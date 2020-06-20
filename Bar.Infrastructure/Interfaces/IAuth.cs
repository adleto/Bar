using Bar.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bar.Infrastructure.Interfaces
{
    public interface IAuth
    {
        Task<ApplicationUserModel> Authenticate(string username, string password);
        Task<ApplicationUserModel> Register(ApplicationUserInsertModel model);
    }
}
