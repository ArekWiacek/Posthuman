using System.Collections.Generic;
using System.Threading.Tasks;
using Posthuman.RealTime.Notifications;
using Microsoft.AspNetCore.SignalR;
using Posthuman.Core.Models.DTO.Avatar;
using Posthuman.Core.Models.Entities;
using System;
using Posthuman.Core.Models.Enums;

namespace Posthuman.Services
{
    public class NotificationsService : INotificationsService
    {
        private IHubContext<NotificationsHub, INotificationsClient> NotificationsContext;
        private List<NotificationMessage> notifications = new List<NotificationMessage>();

        public NotificationsService(
            IHubContext<NotificationsHub, 
            INotificationsClient> notificationsContext)
        {
            NotificationsContext = notificationsContext;
        }
        
        /// <summary>
        /// Creates basic notification based on given text parameters
        /// </summary>
        public NotificationMessage CreateNotification(
            string title, 
            string subtitle = "",
            string text = "", 
            string secondText = "", 
            string reward = "")
        {
            NotificationMessage notification = new NotificationMessage
            {
                Occured = DateTime.Now,
                ShowInModal = false,
                Title = title,
                Subtitle = subtitle,
                Text = text,
                SecondText = secondText,
                Reward = reward
            };

            return notification;
        }

        /// <summary>
        /// Creates notification based on occured EventItem
        /// </summary>
        public NotificationMessage CreateNotification(
            Avatar avatar, 
            EventItem eventItem, 
            TodoItem? todoItem = null)
        {
            if (avatar == null)
                throw new ArgumentNullException("avatar");

            if (eventItem == null)
                throw new ArgumentNullException("eventItem");


            NotificationMessage notification = new NotificationMessage
            {
                Occured = DateTime.Now,
                AvatarName = avatar.Name,
                ShowInModal = false
            };

            string title = "";
            string text = "";
            string secondText = "";
            string reward = "";

            switch (eventItem.Type)
            {
                case EventType.TodoItemCreated:
                    title = "Task created";
                    text = $"{avatar.Name} created new task: '{todoItem.Title}'";
                    break;

                //case EventType.TodoItemCyclicCreated:
                //    title = "Repeating task created";
                //    text = $"{avatar.Name} created new repeating task: '{todoItem.Title}'";
                //    break;

                case EventType.TodoItemDeleted:
                    title = "Task deleted";
                    text = $"{avatar.Name} deleted task: '{todoItem.Title}'";
                    break;

                case EventType.TodoItemModified:
                    title = "Task modified";
                    text = $"{avatar.Name} modified task: '{todoItem.Title}'";
                    break;

                case EventType.TodoItemCompleted:
                    title = "Task completed";
                    reward = $"+ {eventItem.ExpGained} XP";
                    //  notification.Subtitle = $"Jebane +{eventItem.ExpGained} expa!";
                    text = $"{avatar.Name} gained +{eventItem.ExpGained} experience points for completing task: '{todoItem.Title}'";
                    secondText = $"Some second text";
                    break;

                case EventType.AvatarLevelGained:
                    title = $"{avatar.Name}";
                    reward = $"Level {avatar.Level}";
                    //notification.Subtitle = $"Level {avatar.Level}";
                    text = $"{avatar.Name} reached {avatar.Level}. Next level at {avatar.ExpToNewLevel} XP.";
                    secondText = $"Some second text";
                    break;

                case EventType.CardDiscovered:
                    title = "New card discovered!";
                    reward = "New card";
                    text = $"{avatar.Name} discovered new card. Go to cards page to see it!.";
                    break;
            }

            notification.Title = title;
            notification.Text = text;
            notification.SecondText = secondText;
            notification.Reward = reward;

            return notification;
        }

        public void AddNotification(NotificationMessage notification)
        {
            notifications.Add(notification);
        }

        public void AddNotifications(IEnumerable<NotificationMessage> notifications)
        {
            this.notifications.AddRange(notifications);
        }

        public async Task SendNotification(NotificationMessage notification)
        {
            await NotificationsContext.Clients.All.ReceiveNotification(notification);

            if (notifications.Contains(notification))
                notifications.Remove(notification);
        }

        public async Task SendNotifications(IEnumerable<NotificationMessage> notifications)
        {
            foreach (var notification in notifications)
            {
                await NotificationsContext.Clients.All.ReceiveNotification(notification);
            }

            // TODO: add removing notifications that has been sent
        }

        public async Task SendAllNotifications()
        {
            if (notifications.Count == 0)
                return;

            foreach (var notification in notifications)
            {
                await NotificationsContext.Clients.All.ReceiveNotification(notification);
            }

            notifications.Clear();
        }

        // TODO - move it to new class?
        public async Task SendUpdateAvatarNotification(AvatarDTO avatarDTO)
        {
            await NotificationsContext.Clients.All.ReceiveAvatarUpdate(avatarDTO);
        }
    }
}
