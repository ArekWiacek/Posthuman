﻿using AutoMapper;
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

namespace Posthuman.Services
{
    public class TodoItemsService : ITodoItemsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly INotificationsService notificationsService;
        private readonly IAvatarsService avatarsService;
        private readonly IEventItemsService eventItemsService;
        private readonly ExperienceManager expManager;

        public TodoItemsService(
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

        #region GET
        public async Task<TodoItemDTO> GetTodoItemById(int id)
        {
            var todoItem = await unitOfWork.TodoItems.GetByIdAsync(id);
            return mapper.Map<TodoItemDTO>(todoItem);
        }

        public async Task<IEnumerable<TodoItemDTO>> GetAllTodoItems()
        {
            var allTodoItems = await
                unitOfWork.TodoItems.GetAllAsync();

            return mapper.Map<IEnumerable<TodoItemDTO>>(allTodoItems);
        }

        public async Task<IEnumerable<TodoItemDTO>> GetAllTodoItemsForActiveAvatar()
        {
            var avatar = await unitOfWork.Avatars.GetActiveAvatarAsync();

            var avatarTodoItems = await
                unitOfWork
                .TodoItems
                .GetAllByAvatarIdAsync(avatar.Id);

            // Ordering tasks in flat list mode:
            // First unfinished - all completed goes to end
            // Then those with deadline specified
            // Then by deadline date
            // Then those without deadline specified
            // Then completed tasks
            // Visible / invisible changes nothing here
            var sortedTodoItemsList = avatarTodoItems
                .OrderBy(t => t.IsCompleted)
                .ThenByDescending(t => t.Deadline.HasValue)
                .ThenBy(t => t.Deadline);

            var todoItemsMapped =
                mapper.Map<IEnumerable<TodoItemDTO>>(sortedTodoItemsList);

            return todoItemsMapped.ToList();
        }

        public async Task<IEnumerable<TodoItemDTO>> GetTodoItemsHierarchical()
        {
            var avatar = await unitOfWork.Avatars.GetActiveAvatarAsync();

            var todoItems = await
                unitOfWork
                .TodoItems
                .GetAllByAvatarIdAsync(avatar.Id);

            var topLevelTasks = todoItems.Where(ti => ti.IsTopLevel()).ToList();
            var flattenedTasksList = await FlattenSubtasksListAsync(topLevelTasks);

            var itemsMapped =
                mapper.Map<IEnumerable<TodoItemDTO>>(flattenedTasksList);

            return itemsMapped.ToList();
        }
        #endregion GET

        #region CREATE
        public async Task<TodoItemDTO> CreateTodoItem(TodoItemDTO newTodoItemDTO)
        {
            var newTodoItem = mapper.Map<TodoItem>(newTodoItemDTO);
            newTodoItem.IsCompleted = false;
            newTodoItem.CreationDate = DateTime.Now;

            if (newTodoItem == null)
                throw new Exception();

            // Set owner Avatar, if not provided then set active user
            var ownerAvatar = await
                unitOfWork
                .Avatars
                .GetByIdAsync(newTodoItemDTO.AvatarId);

            if (ownerAvatar != null)
                newTodoItem.Avatar = ownerAvatar;
            else
            {
                ownerAvatar = await unitOfWork.Avatars.GetActiveAvatarAsync();
                newTodoItem.Avatar = ownerAvatar;
            }

            // Has parent todo item?
            if (newTodoItem != null && newTodoItem.ParentId.HasValue)
            {
                var parentTodoItem = await unitOfWork.TodoItems.GetByIdAsync(newTodoItem.ParentId.Value);
                if (parentTodoItem == null)
                    throw new Exception();

                newTodoItem.Parent = parentTodoItem;
            }

            // New TodoItem has parent Project selected - so update subtasks counter 
            if (newTodoItem != null && newTodoItem.ProjectId.HasValue)
            {
                var parentProject = await
                    unitOfWork
                    .Projects
                    .GetByIdAsync(newTodoItem.ProjectId.Value);

                if (parentProject != null)
                {
                    parentProject.TotalSubtasks += 1;
                }
            }

            // Create task cycle (repetition info)
            if(newTodoItem != null && newTodoItem.IsCyclic)
            {
                var cycleInfo = CreateCycleInfo(newTodoItemDTO);
                CalculateCycleInfoInstances(cycleInfo);
                newTodoItem.CycleInfo = cycleInfo;
                await unitOfWork.TodoItemsCycles.AddAsync(cycleInfo);
            }

            // TODO - remove commit from here; now it's added so event item can save created todo item ID
            await unitOfWork.TodoItems.AddAsync(newTodoItem);
            await unitOfWork.CommitAsync();

            var todoItemCreatedEvent = await eventItemsService.AddNewEvent(
                ownerAvatar.Id,
                newTodoItem.IsCyclic ? EventType.TodoItemCyclicCreated : EventType.TodoItemCreated,
                EntityType.TodoItem,
                newTodoItem.Id);

            await unitOfWork.CommitAsync();

            notificationsService.AddNotification(NotificationsHelper.CreateNotification(ownerAvatar, todoItemCreatedEvent, newTodoItem));
            await notificationsService.SendAllNotifications();

            return mapper.Map<TodoItemDTO>(newTodoItem);
        }
        #endregion CREATE

        #region UPDATE
        public async Task UpdateTodoItem(TodoItemDTO updatedTodoItemDTO)
        {
            if (updatedTodoItemDTO != null && updatedTodoItemDTO.Id != 0)
            {
                var todoItem = await unitOfWork.TodoItems.GetByIdWithSubtasksAsync(updatedTodoItemDTO.Id);
                var ownerAvatar = await unitOfWork.Avatars.GetActiveAvatarAsync();

                if (todoItem == null)
                    throw new ArgumentNullException("TodoItem", $"TodoItem of ID: {updatedTodoItemDTO.Id} could not be found.");

                if (ownerAvatar == null)
                    throw new ArgumentNullException("Avatar", "Task owner could not be found");

                todoItem.Title = updatedTodoItemDTO.Title;
                todoItem.Description = updatedTodoItemDTO.Description;

                // Deadline changed - update for todoItem and all children
                if (todoItem.Deadline != updatedTodoItemDTO.Deadline)
                    await UpdateTodoItemDeadline(todoItem, updatedTodoItemDTO.Deadline, true);

                // Visibility changed - update for todoItem and all children
                if (todoItem.IsVisible != updatedTodoItemDTO.IsVisible)
                    await UpdateTodoItemVisibility(todoItem, updatedTodoItemDTO.IsVisible);

                // Parent Project was changed - update both old and new parent
                if (todoItem.ProjectId != updatedTodoItemDTO.ProjectId)
                {
                    if (todoItem.ProjectId.HasValue)
                    {
                        var oldParentProject = await unitOfWork.Projects.GetByIdAsync(todoItem.ProjectId.Value);
                        if (oldParentProject != null)
                            oldParentProject.TotalSubtasks--;
                    }

                    if (updatedTodoItemDTO.ProjectId.HasValue)
                    {
                        var newParentProject = await unitOfWork.Projects.GetByIdAsync(updatedTodoItemDTO.ProjectId.Value);
                        if (newParentProject != null)
                            newParentProject.TotalSubtasks++;
                    }

                    todoItem.ProjectId = updatedTodoItemDTO.ProjectId;
                }

                // Parent todo item changed
                if (todoItem.ParentId != updatedTodoItemDTO.ParentId)
                {
                    // Update parent
                    if (updatedTodoItemDTO.ParentId.HasValue)
                    {
                        var parentTask = await unitOfWork.TodoItems.GetByIdAsync(updatedTodoItemDTO.ParentId.Value);
                        todoItem.Parent = parentTask;
                    }
                    // Or remove parent
                    else
                    {
                        todoItem.Parent = null;
                        todoItem.ParentId = null;
                    }
                }

                // Repetitive task checked / unchecked
                if (todoItem.IsCyclic != updatedTodoItemDTO.IsCyclic)
                {
                    // Repetition added
                    if(updatedTodoItemDTO.IsCyclic)
                    {
                        var cycleInfo = CreateCycleInfo(updatedTodoItemDTO);
                        todoItem.CycleInfo = cycleInfo;
                        await unitOfWork.TodoItemsCycles.UpdateAsync(cycleInfo);
                    }
                    // Repetition removed
                    else
                    {
                        unitOfWork.TodoItemsCycles.Remove(todoItem.CycleInfo);
                    }
                }

                var todoItemModifiedEvent = await eventItemsService.AddNewEvent(
                    ownerAvatar.Id, 
                    EventType.TodoItemModified, 
                    EntityType.TodoItem, 
                    todoItem.Id);

                await unitOfWork.CommitAsync();

                notificationsService.AddNotification(NotificationsHelper.CreateNotification(todoItem.Avatar, todoItemModifiedEvent, todoItem));
                
                await notificationsService.SendAllNotifications();
            }
        }

        private TodoItemCycle CreateCycleInfo(TodoItemDTO todoItemDTO)
        {
            TodoItemCycle cycleInfo = null;

            if (todoItemDTO.IsCyclic)
            {
                cycleInfo = new TodoItemCycle();
                cycleInfo.RepetitionPeriod = (RepetitionPeriod)todoItemDTO.RepetitionPeriod;
                cycleInfo.StartDate = todoItemDTO.StartDate.HasValue ? todoItemDTO.StartDate.Value : DateTime.Now;
                cycleInfo.EndDate = todoItemDTO.EndDate;
                cycleInfo.IsInfinite = !todoItemDTO.EndDate.HasValue;

                return cycleInfo;
            }

            return cycleInfo;
        }

        private void CalculateCycleInfoInstances(TodoItemCycle todoItemCycle)
        {
            if(!todoItemCycle.IsInfinite && todoItemCycle.EndDate.HasValue)
            {
                int instances = 0;
                DateTime startDate = todoItemCycle.StartDate;
                DateTime endDate = todoItemCycle.EndDate.Value;

                switch(todoItemCycle.RepetitionPeriod)
                {
                    case RepetitionPeriod.Daily:
                        instances = (endDate.Date - startDate.Date).Days + 1;
                        break;

                    case RepetitionPeriod.Weekly:
                        instances = (endDate - startDate).Days / 7 + 1;
                        break;

                    case RepetitionPeriod.Monthly:
                        //instances = (endDate)
                        break;

                    default:
                        break;
                }

                // Newly created
                //if(todoItemCycle.Id == 0)
                //{
                //    todoItemCycle.CompletedInstances = 0;
                //    todoItemCycle.InstancesStreak = 0;
                //    todoItemCycle.MissedInstances = 0;
                //}
                // Edited
                //else
                //{
                //}

                todoItemCycle.Instances = instances;
            }
        }

        public async Task CompleteTodoItem(TodoItemDTO todoItemDTO)
        {
            if (todoItemDTO != null && todoItemDTO.Id != 0)
            {
                var todoItem = await unitOfWork.TodoItems.GetByIdAsync(todoItemDTO.Id);
                var avatar = await unitOfWork.Avatars.GetActiveAvatarAsync();

                if (todoItem == null)
                    throw new ArgumentNullException("TodoItem", $"TodoItem of ID: {todoItemDTO.Id} could not be found.");

                if (avatar == null)
                    throw new ArgumentNullException("Avatar", "Task owner could not be found");

                // Check if can be completed
                if (todoItem.HasUnfinishedSubtasks())
                    throw new Exception("Cannot complete TodoItem - complete subtasks first.");

                todoItem.IsCompleted = true;
                todoItem.CompletionDate = DateTime.Now;

                // Add event of completion
                var todoItemCompletedEvent = await eventItemsService.AddNewEvent(
                    avatar.Id,
                    EventType.TodoItemCompleted,
                    EntityType.TodoItem,
                    todoItem.Id);

                var experienceForEvent = expManager.CalculateExperienceForEvent(todoItemCompletedEvent, null);
                todoItemCompletedEvent.ExpGained = experienceForEvent;

                notificationsService.AddNotification(NotificationsHelper.CreateNotification(todoItem.Avatar, todoItemCompletedEvent, todoItem));

                await avatarsService.UpdateAvatarGainedExp(avatar, experienceForEvent);

                await unitOfWork.CommitAsync();

                await notificationsService.SendAllNotifications();

                var owner = mapper.Map<AvatarDTO>(avatar);
                await notificationsService.UpdateAvatar(owner);
            }
        }

        /// <summary>
        /// Updates deadline date of given todo item.
        /// If todo item has children, all children deadline is updated as well
        ///     If new deadline is later than current, then current is preserved
        ///     If new deadline is earlier than current, then new is set
        ///     
        /// TODO: FIX method logic when has subtasks and / or parent
        /// </summary>
        private async Task UpdateTodoItemDeadline(TodoItem todoItem, DateTime? newDeadline, bool isParent)
        {
            if (isParent)
                todoItem.Deadline = newDeadline;
            else
            {
                if (newDeadline < todoItem.Deadline)
                    todoItem.Deadline = newDeadline;
            }

            if (todoItem.HasSubtasks())
                foreach (var subtask in todoItem.Subtasks)
                    await UpdateTodoItemDeadline(subtask, newDeadline, false);
        }

        private async Task UpdateTodoItemVisibility(TodoItem todoItem, bool isVisible)
        {
            todoItem.IsVisible = isVisible;

            if (todoItem.HasSubtasks())
                foreach (var subtask in todoItem.Subtasks)
                    await UpdateTodoItemVisibility(subtask, isVisible);
        }
        #endregion UPDATE

        #region DELETE
        public async Task DeleteTodoItem(int id)
        {
            var todoItem = await unitOfWork.TodoItems.GetByIdWithSubtasksAsync(id);

            if (todoItem == null)
                return;

            if (todoItem.ProjectId != null)
            {
                var parentProject = await unitOfWork.Projects.GetByIdAsync(todoItem.ProjectId.Value);

                if (parentProject != null)
                    parentProject.TotalSubtasks--;
            }

            await DeleteTodoItemWithSubtasks(todoItem);

            await unitOfWork.CommitAsync();

            await notificationsService.SendAllNotifications();
        }

        private async Task DeleteTodoItemWithSubtasks(TodoItem todoItem)
        {
            if (todoItem.HasSubtasks())
            {
                var subtasks = await unitOfWork.TodoItems.GetAllByParentIdAsync(todoItem.Id);

                foreach (var subtask in subtasks)
                {
                    await DeleteTodoItemWithSubtasks(subtask);
                }
            }

            unitOfWork.TodoItems.Remove(todoItem);

            var todoItemDeletedEvent = await eventItemsService.AddNewEvent(
                todoItem.AvatarId,
                EventType.TodoItemDeleted,
                EntityType.TodoItem,
                todoItem.Id);

            notificationsService.AddNotification(NotificationsHelper.CreateNotification(todoItem.Avatar, todoItemDeletedEvent, todoItem));
        }
        #endregion DELETE

        #region ADDITIONAL
        /// <summary>
        /// Converts todo items from nested object structure (.Subtasks) into one-level-deep flat list
        /// </summary>
        private async Task<IEnumerable<TodoItem>> FlattenSubtasksListAsync(IEnumerable<TodoItem> todoItems)
        {
            var newList = new List<TodoItem>();

            var tasksSorted = SortTodoItems(todoItems);

            foreach (var task in tasksSorted)
            {
                newList.Add(task);

                if (task.HasSubtasks())
                {
                    var flattenedSubtasks = await FlattenSubtasksListAsync(task.Subtasks);
                    newList.AddRange(flattenedSubtasks);
                }
            }

            return newList;
        }

        private IOrderedEnumerable<TodoItem> SortTodoItems(IEnumerable<TodoItem> todoItems)
        {
            // Sort todo items
            //
            // First unfinished,
            // Then visible,
            // Then those with deadline provided
            // Then by nearest deadline

            return todoItems
                .OrderBy(t => t.IsCompleted)
                .ThenByDescending(t => t.IsVisible)
                .ThenByDescending(t => t.Deadline.HasValue)
                .ThenBy(t => t.Deadline);
        }
        #endregion
    }
}
