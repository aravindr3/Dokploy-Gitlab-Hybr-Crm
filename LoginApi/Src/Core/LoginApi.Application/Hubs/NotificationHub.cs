using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace HyBrForex.Application.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task SendNotification(string message)
        {
            await Clients.All.SendAsync("ReceiveNotification", message);
        }
    }
}
