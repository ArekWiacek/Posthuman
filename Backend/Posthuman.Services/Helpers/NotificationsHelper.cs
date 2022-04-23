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
            ValidationHelper.CheckIfExists(eventItem);
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
            }

            return notification;
        }

        public static NotificationMessage CreateTodoItemNotification(NotificationMessage notification, Avatar avatar, EventItem eventItem, TodoItem? todoItem = null)
        {
            switch (eventItem.Type)
            {
                case EventType.TodoItemCreated:
                    notification.Title = "Task created";
                    notification.Text = $"{avatar.Name} created new task: '{todoItem.Title}'";
                    break;

                //case EventType.TodoItemCyclicCreated:
                //    notification.Title = "Repeating task created";
                //    notification.Text = $"{avatar.Name} created new repeating task: '{todoItem.Title}'";
                //    break;

                case EventType.TodoItemDeleted:
                    notification.Title = "Task deleted";
                    notification.Text = $"{avatar.Name} deleted task: '{todoItem.Title}'";
                    break;

                case EventType.TodoItemModified:
                    notification.Title = "Task modified";
                    notification.Text = $"{avatar.Name} modified task: '{todoItem.Title}'";
                    break;

                case EventType.TodoItemCompleted:
                    notification.Title = "Task completed";
                    notification.Reward = $"+ {eventItem.ExpGained} XP";
                    //  notification.Subtitle = $"Jebane +{eventItem.ExpGained} expa!";
                    notification.Text = $"{avatar.Name} gained +{eventItem.ExpGained} experience points for completing task: '{todoItem.Title}'";
                    notification.SecondText = $"Some second text";
                    break;

                case EventType.AvatarLevelGained:
                    notification.Title = $"{avatar.Name}";
                    notification.Reward = $"Level {avatar.Level}";
                    //notification.Subtitle = $"Level {avatar.Level}";
                    notification.Text = $"{avatar.Name} reached {avatar.Level}. Next level at {avatar.ExpToNewLevel} XP.";
                    notification.SecondText = $"Some second text";
                    break;

                case EventType.CardDiscovered:
                    notification.Title = "New card discovered!";
                    notification.Reward = "New card";
                    notification.Text = $"{avatar.Name} discovered new card. Go to cards page to see it!.";
                    break;
            }

            return notification;
        }

        public static NotificationMessage CreateHabitNotification(NotificationMessage notification, Avatar avatar, EventItem eventItem, Habit habit)
        {
            switch (eventItem.Type)
            {
                case EventType.HabitCreated:
                    notification.Title = "Habit created";
                    notification.Text = $"{avatar.Name} created new habit: '{habit.Title}'";
                    break;

                case EventType.HabitDeleted:
                    notification.Title = "Habit deleted";
                    notification.Text = $"{avatar.Name} deleted habit: '{habit.Title}'";
                    break;

                case EventType.HabitModified:
                    notification.Title = "Habit modified";
                    notification.Text = $"{avatar.Name} modified habit: '{habit.Title}'";
                    break;

                case EventType.HabitCompleted:
                    notification.Title = "Habit completed";
                    notification.Reward = $"+ {eventItem.ExpGained} XP";
                    //  notification.Subtitle = $"Jebane +{eventItem.ExpGained} expa!";
                    notification.Text = $"{avatar.Name} gained + {eventItem.ExpGained} experience points for completing habit: '{habit.Title}'";
                    notification.SecondText = $"Some second text";
                    break;
            }

            return notification;
        }
    }
}
