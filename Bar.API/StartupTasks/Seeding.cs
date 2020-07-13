using Bar.Database;
using Bar.Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Bar.API.StartupTasks
{
    public class Seeding
    {
        public async static Task Seed(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<Context>();
            if (context.ApplicationUser.Count() == 0)
            {
                var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                string[] roleNames = { "MasterUser", "RegularUser" };
                IdentityResult roleResult;

                foreach (var roleName in roleNames)
                {
                    // creating the roles and seeding them to the database
                    var roleExist = await RoleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }

                var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                ApplicationUser main = new ApplicationUser
                {
                    UserName = "masterUser",
                    Email = "master@zar.com"
                };
                await UserManager.CreateAsync(main, "bestVersionDude5");
                //Add users to roles
                await UserManager.AddToRoleAsync(main, "MasterUser");
            }
            if(context.DatabaseTimeStamp.Count() == 0)
            {
                context.DatabaseTimeStamp.Add(new DatabaseTimeStamp
                {
                    TimeStamp = DateTime.Now
                });
                context.SaveChanges();
            }
        }
        public static void MigrateDatabase(Context context)
        {
            context.Database.Migrate();
        }
    }
}
