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
        private readonly ExperienceHelper expManager;

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
            expManager = new ExperienceHelper();
        }


        public async Task<HabitDTO> GetByUserId(int id, int userId)
        {
            var habit = await unitOfWork.Habits.GetByUserId(id, userId);
            return mapper.Map<HabitDTO>(habit);
        }

        public async Task<IEnumerable<HabitDTO>> GetAllByUserId(int userId)
        {
            var habits = await
                unitOfWork
                .Habits
                .GetAllByUserIdAsync(userId);

            return mapper.Map<IEnumerable<HabitDTO>>(habits).ToList();
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
            newHabit.NextIstanceDate = FindNextInstanceDate(newHabit);
            newHabit.PreviousInstanceDate = null;
            newHabit.LastCompletedInstanceDate = null;
            newHabit.IsActive = newHabit.NextIstanceDate.Date == DateTime.Today ? true : false;

            switch (newHabit.RepetitionPeriod)
            {
                case RepetitionPeriod.Weekly:
                    newHabit.DayOfMonth = 0;
                    break;

                case RepetitionPeriod.Monthly:
                    newHabit.WeekDays = 0;
                    break;

                default:
                    throw new Exception("Wrong type of repetition. Allowed types are: 'Weekly' and 'Monthly'");
            }

            await unitOfWork.Habits.AddAsync(newHabit);
            await unitOfWork.CommitAsync();

            await eventItemsService.AddNewEventItem(userId, EventType.HabitCreated, EntityType.Habit, newHabit.Id);

            await unitOfWork.CommitAsync();

            //notificationsService.AddNotification(NotificationsHelper.CreateNotification(newTodoItem.Avatar, todoItemCreatedEvent, newTodoItem));
            //await notificationsService.SendAllNotifications();

            return mapper.Map<HabitDTO>(newHabit);
        }

        private async Task<IEnumerable<Habit>> GetAll()
        {
            var habits = await
                unitOfWork
                .Habits
                .GetAllAsync();

            return habits;
        }

        public async Task UpdateHabit(CreateHabitDTO habitDTO)
        {
            if (habitDTO != null && habitDTO.Id != 0)
            {
                var habit = await unitOfWork.Habits.GetByIdAsync(habitDTO.Id);

                if (habit == null)
                    throw new ArgumentNullException("Habit", $"Habit of ID: {habitDTO.Id} could not be found.");

                habit.Title = habitDTO.Title;
                habit.Description = habitDTO.Description;
                habit.WeekDays = DaysOfWeekUtils.ValueOf(habitDTO.WeekDays);
                habit.RepetitionPeriod = Enum.Parse<RepetitionPeriod>(habitDTO.RepetitionPeriod);
                await unitOfWork.CommitAsync();
            }
        }

        public async Task DeleteHabit(int id, int userId)
        {
            var habit = await unitOfWork.Habits.GetByIdAsync(id);

            if (habit == null)
                throw new ArgumentNullException("HabitId", $"Cannot find habit of ID: {id}");

            if (habit.UserId != userId)
                throw new Exception($"User with ID: {userId} is not owner of habit with ID: {id}. Access denied. Go fuck yourself.");

            unitOfWork.Habits.Remove(habit);

            await unitOfWork.CommitAsync();
        }

        public async Task CompleteHabitInstance(int id, int userId)
        {
            if (id != 0 && userId != 0)
            {
                var habit = await unitOfWork.Habits.GetByIdAsync(id);
                if (habit == null)
                    throw new ArgumentNullException("Habit", $"Habit of ID: {id} could not be found.");

                if (habit.UserId != userId)
                    throw new Exception($"User with ID: {userId} is not owner of habit with ID: {id}. Access denied. Go fuck yourself.");

                // Skip if not active yet
                if (!habit.IsActive || habit.NextIstanceDate.Date > DateTime.Now.Date)
                {
                    throw new Exception($"Habit of ID: {habit.Id} is not active today so can't be completed.");
                }

                habit.IsActive = false;
                habit.LastCompletedInstanceDate = DateTime.Now.Date;
                habit.CompletedInstances++;
                habit.CurrentStreak++;
                habit.NextIstanceDate = FindNextInstanceDate(habit);
                habit.PreviousInstanceDate = FindPreviousInstanceDate(habit);

                if (habit.CurrentStreak > habit.LongestStreak)
                    habit.LongestStreak = habit.CurrentStreak;

                await eventItemsService.AddNewEventItem(userId, EventType.HabitCompleted, EntityType.Habit, habit.Id);

                await unitOfWork.CommitAsync();

                //notificationsService.AddNotification(NotificationsHelper.CreateNotification(habit.Avatar, habitCompletedEvent));
                //await notificationsService.SendAllNotifications();
            }
        }
        public async Task ProcessAllHabitsMissedInstances()
        {
            var allHabits = await GetAll();
            allHabits.ToList().ForEach(habit => ProcessMissedInstances(habit));
            await unitOfWork.CommitAsync();
        }

        private void ProcessMissedInstances(Habit habit)
        {
            // Missed instance - should be completed earlier
            if (habit.NextIstanceDate < DateTime.Today)
            {
                habit.MissedInstances++;
                habit.CurrentStreak = 0;
                habit.NextIstanceDate = FindNextInstanceDate(habit);
                habit.PreviousInstanceDate = FindPreviousInstanceDate(habit);
                habit.IsActive = habit.NextIstanceDate == DateTime.Today ? true : false;
            }
            // Today is the time so activate habit
            else if (habit.NextIstanceDate == DateTime.Today && !habit.IsActive)
            {
                habit.IsActive = true;
            }
        }

        private DateTime FindPreviousInstanceDate(Habit habit)
        {
            DateTime previousInstanceDate = DateTime.MinValue;

            switch (habit.RepetitionPeriod)
            {
                case RepetitionPeriod.Weekly:
                    var days = DaysOfWeekUtils.DaysOfWeek(habit.WeekDays);
                    DateTime previousInstance = DateTime.Now.Date.AddDays(-1);
                    while (!days.Contains(previousInstance.DayOfWeek))
                    {
                        previousInstance = previousInstance.AddDays(-1);
                    }

                    previousInstanceDate = previousInstance.Date;
                    break;

                case RepetitionPeriod.Monthly:
                    break;

                default:
                    break;
            }

            return previousInstanceDate;
        }

        private DateTime FindNextInstanceDate(Habit habit)
        {
            DateTime nextInstanceDate = habit.NextIstanceDate;

            switch (habit.RepetitionPeriod)
            {
                case RepetitionPeriod.Weekly:
                    var days = DaysOfWeekUtils.DaysOfWeek(habit.WeekDays);

                    // Scenario 1:
                    //      - Todays day of week is on the repetition days list
                    //      - It was not completed today
                    //          - That means that next available instance date is now
                    if (days.Contains(DateTime.Now.DayOfWeek) && !WasCompletedToday(habit))
                        nextInstanceDate = DateTime.Today;

                    // Scenario 2:
                    //      - Todays date not on the list
                    //      - Add one day and check if then on the list
                    //      - If yes, then this is nearest date
                    else
                    {
                        DateTime nextInstance = DateTime.Now.Date.AddDays(1);
                        while (!days.Contains(nextInstance.DayOfWeek))
                        {
                            nextInstance = nextInstance.AddDays(1);
                        }

                        nextInstanceDate = nextInstance.Date;
                    }

                    break;

                case RepetitionPeriod.Monthly:
                    //var today = DateTime.Now.Date;
                    //if (today >= habit.NextIstanceDate)
                    //    return habit;
                    break;
            }

            return nextInstanceDate;
        }

        private bool WasCompletedToday(Habit habit)
        {
            return habit.LastCompletedInstanceDate.HasValue &&
                    habit.LastCompletedInstanceDate.Value.Date == DateTime.Now.Date ? true : false;
        }

        private bool WasLastInstanceCompleted(Habit habit)
        {
            return
                habit.PreviousInstanceDate.HasValue &&
                habit.LastCompletedInstanceDate.HasValue &&
                habit.PreviousInstanceDate.Value.Date == habit.LastCompletedInstanceDate.Value.Date;
        }
    }
}
