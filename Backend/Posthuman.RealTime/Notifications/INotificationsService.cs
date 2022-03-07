using System.Collections.Generic;
using System.Threading.Tasks;
using Posthuman.Core.Models.DTO.Avatar;
using Posthuman.Core.Models.Entities;

namespace Posthuman.RealTime.Notifications
{
    public interface INotificationsService
    {
        /// <summary>
        /// Creates basic notification based on given text parameters
        /// </summary>
        NotificationMessage CreateNotification(
            string title,
            string subtitle = "",
            string text = "",
            string secondText = "",
            string reward = "");

        /// <summary>
        /// Creates notification based on occured EventItem
        /// </summary>
        NotificationMessage CreateNotification(
            Avatar avatar,
            EventItem eventItem,
            TodoItem? todoItem = null);

        /// <summary>
        /// Adds single notification to notifications collection
        /// </summary>
        /// <param name="notification"></param>
        void AddNotification(NotificationMessage notification);

        /// <summary>
        /// Adds multiple notifications to notifications collection
        /// </summary>
        void AddNotifications(IEnumerable<NotificationMessage> notifications);

        /// <summary>
        /// Sends single notification to receivers
        /// </summary>
        Task SendNotification(NotificationMessage notification);
        
        /// <summary>
        /// Sends multiple notifications to receivers
        /// </summary>
        /// <param name="notifications"></param>
        /// <returns></returns>
        Task SendNotifications(IEnumerable<NotificationMessage> notifications);
        
        /// <summary>
        /// Sends all notifications to receivers
        /// </summary>
        /// <returns></returns>
        Task SendAllNotifications();

        /// <summary>
        /// Sends notification with updated avatar properties 
        /// Used e.g. when avatar gains XP or new level
        /// </summary>
        Task SendUpdateAvatarNotification(AvatarDTO avatarDTO);
    }
}
