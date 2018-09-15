using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace DEMO_AzureSignalRService.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public override Task OnDisconnectedAsync(Exception exception)
        {
            return Clients.All.SendAsync("onServerSent", "SERVER", $"{Context.User.Identity.Name} has disconnected.");
        }

        public override Task OnConnectedAsync()
        {
            return Clients.All.SendAsync("onServerSent", "SERVER", $"{Context.User.Identity.Name} has connected.");
        }

        public void OnClientSent(string message)
        {
            Clients.All.SendAsync("onServerSent", Context.User.Identity.Name, message);
        }
    }
}
