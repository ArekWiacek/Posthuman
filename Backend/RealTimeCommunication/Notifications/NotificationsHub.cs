using Microsoft.AspNetCore.SignalR;

namespace Posthuman.RealTimeCommunication.Notifications
{
    public class NotificationsHub : Hub<INotificationsClient>
    {
    }
}
