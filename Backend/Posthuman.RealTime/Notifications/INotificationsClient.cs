using System.Threading.Tasks;
using Posthuman.Core.Models.DTO.Avatar;

namespace Posthuman.RealTime.Notifications
{
    public interface INotificationsClient
    {
        Task ReceiveNotification(NotificationMessage message);

        Task ReceiveAvatarUpdate(AvatarDTO avatarDTO);
    }
}
