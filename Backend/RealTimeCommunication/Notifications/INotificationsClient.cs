using System.Threading.Tasks;

namespace Posthuman.RealTimeCommunication.Notifications
{
    public interface INotificationsClient
    {
        Task ReceiveNotification(NotificationMessage message);
    }
}
