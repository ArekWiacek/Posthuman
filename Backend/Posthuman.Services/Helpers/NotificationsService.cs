using System;
using Posthuman.Core.Models.Entities;
using Posthuman.Core.Models.Enums;
using Posthuman.RealTimeCommunication.Notifications;

namespace Posthuman.Services.Helpers
{
    internal static class NotificationsService
    {
        public static NotificationMessage CreateNotification(Avatar avatar, EventItem eventItem)
        {
            if(avatar == null)
                throw new ArgumentNullException("avatar");

            if (eventItem == null)
                throw new ArgumentNullException("eventItem");


            NotificationMessage notification = new NotificationMessage
            {
                Occured = DateTime.Now,
                AvatarName = avatar.Name,
                ShowInModal = false
            };
            
            switch (eventItem.Type)
            {
                case EventType.TodoItemCompleted:
                    notification.Title = "Task completed";
                    notification.Subtitle = $"+ {eventItem.ExpGained} XP";
                    notification.Text = $"{avatar.Name} gained +{eventItem.ExpGained} experience points for completing task: '{eventItem.RelatedEntityId}'";
                    notification.SecondText = $"Some second text";
                    break;

                case EventType.LevelGained:
                    notification.Title = "New level reached";
                    notification.Subtitle = $"Level {avatar.Level}";
                    notification.Text = $"{avatar.Name} reached {avatar.Level}. Next level at {avatar.ExpToNewLevel} XP.";
                    notification.SecondText = $"Some second text";
                    break;
            }

            return notification;
        }
    }
}
