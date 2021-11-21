using System;
using Posthuman.Core.Models.Entities;
using Posthuman.Core.Models.Enums;
using Posthuman.RealTimeCommunication.Notifications;

namespace Posthuman.Services.Helpers
{
    internal static class NotificationsHelper
    {
        public static NotificationMessage CreateNotification(Avatar avatar, 
            string title, string subtitle = "", 
            string text = "", string secondText = "", string reward = "") 
        {
            if (avatar == null)
                throw new ArgumentNullException("avatar");

            NotificationMessage notification = new NotificationMessage
            {
                Occured = DateTime.Now,
                AvatarName = avatar.Name,
                ShowInModal = false,
                Title = title,
                Subtitle = subtitle,
                Text = text,
                SecondText = secondText,
                Reward = reward
            };

            return notification;
        }

        public static NotificationMessage CreateNotification(Avatar avatar, EventItem eventItem, TodoItem? todoItem = null)
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
                case EventType.TodoItemCreated:
                    notification.Title = "Task created";
                    notification.Text = $"{avatar.Name} created new task: '{todoItem.Title}'";
                    break;

                case EventType.TodoItemDeleted:
                    notification.Title = "Task deleted";
                    notification.Text = $"{avatar.Name} deleted task: '{todoItem.Title}'";
                    break;


                case EventType.TodoItemCompleted:
                    notification.Title = "Task completed";
                    notification.Reward = $"+ {eventItem.ExpGained} XP";
                    //  notification.Subtitle = $"Jebane +{eventItem.ExpGained} expa!";
                    notification.Text = $"{avatar.Name} gained +{eventItem.ExpGained} experience points for completing task: '{todoItem.Title}'";
                    notification.SecondText = $"Some second text";
                    break;

                case EventType.LevelGained:
                    notification.Title = $"{avatar.Name}";
                    notification.Reward = $"Level {avatar.Level}";
                    //notification.Subtitle = $"Level {avatar.Level}";
                    notification.Text = $"{avatar.Name} reached {avatar.Level}. Next level at {avatar.ExpToNewLevel} XP.";
                    notification.SecondText = $"Some second text";
                    break;
            }

            return notification;
        }
    }
}
