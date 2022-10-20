using Microsoft.AspNetCore.SignalR;
using WASMAspHosted.Shared;

namespace WASMAspHosted.Server.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(Message message)
        {
            await Clients.All.SendAsync("receiveMessage", message);
        }
    }
}
