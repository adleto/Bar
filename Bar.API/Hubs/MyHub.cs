using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bar.API.Hubs
{
    public class MyHub : Hub
    {
        public async Task SendMessage(/*string user, string message*/)
        {
            //await Clients.All.SendAsync("RefreshMessage", user, message);
            await Clients.All.SendAsync("RefreshMessage");
        }
    }
}
