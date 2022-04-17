using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Posthuman.Core;
using Posthuman.Core.Models.DTO;
using Posthuman.Core.Models.Entities;
using Posthuman.Core.Models.Enums;
using Posthuman.Core.Services;
using Posthuman.Services.Helpers;
using Posthuman.RealTime.Notifications;
using Posthuman.Core.Models.DTO.Avatar;
using Posthuman.Core.Models.DTO.Habit;

namespace Posthuman.Services
{
    public class HabitsService : IHabitsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly INotificationsService notificationsService;
        private readonly IAvatarsService avatarsService;
        private readonly IEventItemsService eventItemsService;
        private readonly ExperienceManager expManager;

        public HabitsService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            INotificationsService notificationsService,
            IAvatarsService avatarsService,
            IEventItemsService eventItemsService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.notificationsService = notificationsService;
            this.avatarsService = avatarsService;
            this.eventItemsService = eventItemsService;
            expManager = new ExperienceManager();
        }

        public async Task<IEnumerable<HabitDTO>> GetAllHabits(int userId)
        {
            var avatar = await unitOfWork.Avatars.GetAvatarForUserAsync(userId);
            var habits = await GetAllHabitsForAvatar(avatar);
            return habits;
        }

        public async Task<HabitDTO> CreateHabit(int userId, CreateHabitDTO habitDTO)
        {
            var avatar = await unitOfWork.Avatars.GetAvatarForUserAsync(userId);
            if (avatar == null)
                throw new Exception($"User of ID {userId} does not have avatar created");

            var newHabit = mapper.Map<Habit>(habitDTO);
            newHabit.CreationDate = DateTime.Now;
            newHabit.UserId = userId;
            newHabit.Avatar = avatar;

            await unitOfWork.Habits.AddAsync(newHabit);
            await unitOfWork.CommitAsync();

            //var habitCreatedEvent = await eventItemsService.CreateEventItem(
            //    userId,
            //    EventType.TodoItemCreated,
            //    EntityType.TodoItem,
            //    newTodoItem.Id);

            //await unitOfWork.EventItems.AddAsync(todoItemCreatedEvent);
            //await unitOfWork.CommitAsync();

            //notificationsService.AddNotification(NotificationsHelper.CreateNotification(newTodoItem.Avatar, todoItemCreatedEvent, newTodoItem));
            //await notificationsService.SendAllNotifications();

            return mapper.Map<HabitDTO>(newHabit);
        }

        public async Task UpdateHabit(CreateHabitDTO habitDTO)
        {
            if (habitDTO != null && habitDTO.Id != 0)
            {
                var habit = await unitOfWork.Habits.GetByIdAsync(habitDTO.Id);
                //var ownerAvatar = await unitOfWork.Avatars.GetByIdAsync(1017);

                //if (habit == null)
                //    throw new ArgumentNullException("TodoItem", $"TodoItem of ID: {updatedTodoItemDTO.Id} could not be found.");

                //if (ownerAvatar == null)
                //    throw new ArgumentNullException("Avatar", "Task owner could not be found");

                habit.Title = habitDTO.Title;
                habit.Description = habitDTO.Description;
   
                //var todoItemModifiedEvent = await eventItemsService.CreateEventItem(
                //    ownerAvatar.Id,
                //    EventType.TodoItemModified,
                //    EntityType.TodoItem,
                //    todoItem.Id);

                await unitOfWork.CommitAsync();
            }
        }

        public async Task DeleteHabit(int id)
        {
            var habit = await unitOfWork.Habits.GetByIdAsync(id);

            if (habit == null)
                return;

            unitOfWork.Habits.Remove(habit);
            await unitOfWork.CommitAsync();
        }

        public async Task CompleteHabitInstance(HabitDTO habitDTO)
        {
        }

        private async Task<IEnumerable<HabitDTO>> GetAllHabitsForAvatar(Avatar avatar)
        {
            var avatarHabits = await
                unitOfWork
                .Habits
                .GetAllByUserIdAsync(avatar.Id);

            var habitsMapped =
                mapper.Map<IEnumerable<HabitDTO>>(avatarHabits);

            return habitsMapped.ToList();
        }
    }
}
