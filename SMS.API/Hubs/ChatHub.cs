using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SMS.BLL.Data_Transfer_Objects;
using SMS.BLL.Services.Contracts;

namespace SMS.API.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMessageCrudService _messageCrudService;

        public ChatHub(IMessageCrudService messageCrudService)
        {
            _messageCrudService = messageCrudService;
        }

        public override async Task OnConnectedAsync()
        {
            await Console.Out.WriteLineAsync("Connected.");
        }

        public async Task SendMessage(MessageDto message)
        {
            await Clients.All.SendAsync("receiveMessage", message);
        }
    }
}
