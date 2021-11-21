using Posthuman.Core.Models.DTO;
using System.Threading.Tasks;

namespace Posthuman.RealTimeCommunication.Notifications
{
    public interface INotificationsClient
    {
        Task ReceiveNotification(NotificationMessage message);

        Task ReceiveAvatarUpdate(AvatarDTO avatarDTO);
    }
}
