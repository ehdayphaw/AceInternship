using Microsoft.AspNetCore.SignalR;

namespace AceInternship.ChatApp.Hubs
{
    public class ChatHub : Hub
    {
        public async Task ServerReceiveSendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ClientReceiveMessage", user, message);
        }
    }
}
