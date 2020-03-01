using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace RoomsManager.myHub
{
    public class myHub : Hub
    {
        public async Task sendMessage(string message)
        {
            await Clients.All.SendAsync(message);
        }
    }
}
