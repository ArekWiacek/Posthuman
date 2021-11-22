using Microsoft.AspNetCore.SignalR;

namespace Posthuman.RealTime.Notifications
{
    public class NotificationsHub : Hub<INotificationsClient>
    {
    }
}
