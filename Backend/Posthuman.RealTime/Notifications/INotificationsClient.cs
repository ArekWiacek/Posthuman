using Posthuman.Core.Models.DTO;
using System.Threading.Tasks;

namespace Posthuman.RealTime.Notifications
{
    public interface INotificationsClient
    {
        Task ReceiveNotification(NotificationMessage message);

        Task ReceiveAvatarUpdate(AvatarDTO avatarDTO);
    }
}
