using System;
using Posthuman.Core.Models.Entities;
using Posthuman.Core.Models.Entities.Interfaces;
using Posthuman.Core.Models.Enums;
using Posthuman.RealTime.Notifications;

namespace Posthuman.Services.Helpers
{
    internal static class NotificationsHelper
    {
        public static NotificationMessage CreateNotification<TEntityType>(Avatar avatar, EventItem eventItem, IEntity entity)
        {
            ValidationHelper.CheckIfExists(avatar);
            //ValidationHelper.CheckIfExists(eventItem);
            ValidationHelper.CheckIfExists(entity);

            NotificationMessage notification = new NotificationMessage
            {
                Occured = DateTime.Now,
                AvatarName = avatar.Name,
                ShowInModal = false
            };

            var entityTypeName = entity.GetType().Name;
            var entityType = Enum.Parse(typeof(EntityType), entityTypeName);

            switch (entityType)
            {
                case EntityType.TodoItem:
                    var todoItem = (TodoItem)entity;
                    notification = CreateTodoItemNotification(notification, avatar, eventItem, todoItem);
                    break;

                case EntityType.Habit:
                    var habit = (Habit)entity;
                    notification = CreateHabitNotification(notification, avatar, eventItem, habit);
                    break;

                case EntityType.Avatar:
                    notification = CreateAvatarNotification(notification, avatar, eventItem);
                    break;

                case EntityType.TechnologyCard:
                case EntityType.TechnologyCardDiscovery:
                    notification = CreateCardNotification(notification, avatar, eventItem);
                    break;
            }

            return notification; 
        }

        public static NotificationMessage CreateTodoItemNotification(NotificationMessage notification, Avatar avatar, EventItem eventItem, TodoItem? todoItem = null)
        {
            switch (eventItem.Type)
            {
                case EventType.TodoItemCreated:
                    CreateNotificationTextLabels(notification, "Task created", $"{avatar.Name} created new task: '{todoItem.Title}'.");
                    break;

                case EventType.TodoItemDeleted:
                    CreateNotificationTextLabels(notification, "Task deleted", $"{avatar.Name} deleted task: '{todoItem.Title}'.");
                    break;

                case EventType.TodoItemModified:
                    CreateNotificationTextLabels(notification, "Task modified", $"{avatar.Name} modified task: '{todoItem.Title}'.");
                    break;

                case EventType.TodoItemCompleted:
                    CreateNotificationTextLabels(notification, "Task completed", $"{avatar.Name} completed task: '{todoItem.Title}'.");
                    notification.Title = "Task completed";
                    break;
            }

            CreateNotificationExperienceLabels(notification, eventItem);

            return notification;
        }

        public static NotificationMessage CreateAvatarNotification(NotificationMessage notification, Avatar avatar, EventItem eventItem)
        {
            switch (eventItem.Type)
            {
                case EventType.AvatarLevelGained:
                    CreateNotificationTextLabels(notification, "New level!", $"{avatar.Name} reached {avatar.Level} level! Next level at {avatar.ExpToNewLevel} XP.");
                    notification.Reward = $"Level {avatar.Level}";
                    break;
            }

            return notification;
        }

        public static NotificationMessage CreateCardNotification(NotificationMessage notification, Avatar avatar, EventItem eventItem)
        {
            switch (eventItem.Type)
            {
                case EventType.CardDiscovered:
                    CreateNotificationTextLabels(notification, "New card discovered!", $"{avatar.Name} discovered new card. Go to cards page to see it!.");
                    notification.Reward = "New card";
                    break;
            }

            return notification;
        }

        public static NotificationMessage CreateHabitNotification(NotificationMessage notification, Avatar avatar, EventItem eventItem, Habit habit)
        {
            switch (eventItem.Type)
            {
                case EventType.HabitCreated:
                    CreateNotificationTextLabels(notification, "Habit created", $"{avatar.Name} created new habit: '{habit.Title}'");
                    break;

                case EventType.HabitDeleted:
                    CreateNotificationTextLabels(notification, "Habit deleted", $"{avatar.Name} deleted habit: '{habit.Title}'");
                    break;

                case EventType.HabitModified:
                    CreateNotificationTextLabels(notification, "Habit modified", $"{avatar.Name} modified habit: '{habit.Title}'");
                    break;

                case EventType.HabitCompleted:
                    CreateNotificationTextLabels(notification, "Habit completed", $"{avatar.Name} completed habit: '{habit.Title}'.");
                    break;
            }

            CreateNotificationExperienceLabels(notification, eventItem);

            return notification;
        }

        private static void CreateNotificationTextLabels(NotificationMessage notification, string title, string text)
        {
            notification.Title = title;
            notification.Text = text;
        }

        private static void CreateNotificationExperienceLabels(NotificationMessage notification, EventItem eventItem)
        {
            if (eventItem.ExpGained > 0)
            {
                notification.Reward = $"+ {eventItem.ExpGained} XP";
                notification.Text += $" + {eventItem.ExpGained} XP gained!";
            }
        }
    }
}
