using Posthuman.Core.Models.DTO;
using System.Threading.Tasks;

namespace Posthuman.RealTimeCommunication.Notifications
{
    public interface INotificationsService
    {
        Task UpdateAvatar(AvatarDTO avatarDTO);
        void AddNotification(NotificationMessage notification);
        Task SendAllNotifications();
    }
}
