using Posthuman.Core.Models.DTO;
using System.Threading.Tasks;

namespace Posthuman.RealTime.Notifications
{
    public interface INotificationsService
    {
        Task UpdateAvatar(AvatarDTO avatarDTO);
        void AddNotification(NotificationMessage notification);
        Task SendAllNotifications();
    }
}
