using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Posthuman.WebApi.Hubs
{
    public class ChatMessage
    {

        public string User { get; set; }

        public string Message { get; set; }
    }
    public class PosthumanHub : Hub
    {
        public async Task SendMessage(ChatMessage message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
