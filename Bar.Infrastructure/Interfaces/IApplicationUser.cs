using Bar.Models.Account;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bar.Infrastructure.Interfaces
{
    public interface IApplicationUser
    {
        List<UserViewModel> GetUsers();
        Task<UserViewModel> GetUser(string id);
        Task UserAdd(UserViewModel model);
        Task UserEdit(UserViewModel model);
        Task UserDelete(string id);
    }
}
