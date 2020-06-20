using Bar.Database;
using Bar.Database.Entities;
using Bar.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bar.API.StartupTasks
{
    public class Seeding
    {
        public static void Seed(Context context)
        {
            //var _orderService = serviceProvider.GetRequiredService<IBaseCrudService<Order, Order, Order, Order>>();
            //await _orderService.Insert(new Order
            //{
            //    TimeOfOrder = DateTime.Now
            //});
            //await _orderService.Insert(new Order
            //{
            //    TimeOfOrder = DateTime.Now
            //});
            if (context.Role.Count() == 0)
            {
                var role = new Role
                {
                    Name = "MasterUser"
                };
                context.Role.Add(role);
                var role2 = new Role
                {
                    Name = "RegularUser"
                };
                context.Role.Add(role2);
                var user = new ApplicationUser
                {
                    Username = "konobar1",
                    Role = role,
                    PasswordSalt = AuthHelper.GenerateSalt()
                };
                user.PasswordHash = AuthHelper.GenerateHash(user.PasswordSalt, "superCoolMe2");
                context.ApplicationUser.Add(user);
                context.SaveChanges();
                //THISTHIISISI
                //var item = new Item
                //{
                //    Naziv = "Kafa",
                //    Price = 1
                //};
                //context.Add(item);
                //context.SaveChanges();
                //var order = new Order
                //{
                //    ApplicationUser = user,
                //    TimeOfOrder = DateTime.Now,
                //    ItemOrderList = new List<ItemOrder>()
                //};
                //order.ItemOrderList.Add(new ItemOrder
                //{
                //    Item = item,
                //    Quantity = 5,
                //    Order = order
                //});
                //context.Add(order);
                //HEHREHREHR
                //context.SaveChanges();
            }
        }
        public static void MigrateDatabase(Context context)
        {
            context.Database.Migrate();
        }
    }
}
