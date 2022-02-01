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

namespace Posthuman.Services
{
    public class TodoItemsService : ITodoItemsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ITodoItemsCyclesService todoItemsCyclesService;
        private readonly INotificationsService notificationsService;
        private readonly IAvatarsService avatarsService;
        private readonly IEventItemsService eventItemsService;
        private readonly ExperienceManager expManager;

        public TodoItemsService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ITodoItemsCyclesService todoItemsCyclesService,
            INotificationsService notificationsService,
            IAvatarsService avatarsService,
            IEventItemsService eventItemsService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.todoItemsCyclesService = todoItemsCyclesService;
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
    
        public async Task<IEnumerable<TodoItemDTO>> GetAllTodoItems(int userId)
        {
            var avatar = await unitOfWork.Avatars.GetAvatarForUserAsync(userId);

            var todoItems = await GetAllTodoItemsForAvatar(avatar);

            return todoItems;
        }

        private async Task<IEnumerable<TodoItemDTO>> GetAllTodoItemsForAvatar(Avatar avatar)
        {
            var avatarTodoItems = await
                unitOfWork
                .TodoItems
                .GetAllByUserIdAsync(avatar.Id);

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

        public async Task<IEnumerable<TodoItemDTO>> GetAllTodoItemsHierarchical(int userId)
        {
            var avatar = await unitOfWork.Avatars.GetAvatarForUserAsync(userId);

            var todoItems = await
                unitOfWork
                .TodoItems
                .GetAllByUserIdAsync(userId);

            var topLevelTasks = todoItems.Where(ti => ti.IsTopLevel()).ToList();
            var flattenedTasksList = await FlattenSubtasksListAsync(topLevelTasks);

            var itemsMapped =
                mapper.Map<IEnumerable<TodoItemDTO>>(flattenedTasksList);

            return itemsMapped.ToList();
        }
        #endregion GET

        #region CREATE
        public async Task<TodoItemDTO> CreateTodoItem(int userId, CreateTodoItemDTO createTodoItemDTO)
        {
            var avatar = await unitOfWork.Avatars.GetAvatarForUserAsync(userId);
            if (avatar == null)
                throw new Exception($"User of ID {userId} does not have avatar created");

            var newTodoItem = mapper.Map<TodoItem>(createTodoItemDTO);
            newTodoItem.IsCompleted = false;
            newTodoItem.CreationDate = DateTime.Now;
            newTodoItem.IsVisible = true;
            newTodoItem.UserId = userId;
            newTodoItem.Avatar = avatar;
            
            // Has parent todo item?
            if (newTodoItem.ParentId.HasValue)
            {
                var parentId = newTodoItem.ParentId.Value;
                var parentTodoItem = await unitOfWork.TodoItems.GetByIdAsync(parentId);
                if (parentTodoItem == null)
                    throw new ArgumentException($"Parent for todo item ID: {parentId} not found");

                newTodoItem.ParentId = parentId;
                newTodoItem.Parent = parentTodoItem;
            }

            // TODO - remove commit from here; now it's added so event item can save created todo item ID
            await unitOfWork.TodoItems.AddAsync(newTodoItem);
            await unitOfWork.CommitAsync();

            var todoItemCreatedEvent = await eventItemsService.CreateEventItem(
                userId,
                EventType.TodoItemCreated,
                EntityType.TodoItem,
                newTodoItem.Id);

            await unitOfWork.EventItems.AddAsync(todoItemCreatedEvent);
            await unitOfWork.CommitAsync();

            notificationsService.AddNotification(NotificationsHelper.CreateNotification(newTodoItem.Avatar, todoItemCreatedEvent, newTodoItem));
            await notificationsService.SendAllNotifications();

            return mapper.Map<TodoItemDTO>(newTodoItem);
        }
        #endregion CREATE

        #region UPDATE
        public async Task UpdateTodoItem(TodoItemDTO updatedTodoItemDTO)
        {
            if (updatedTodoItemDTO != null && updatedTodoItemDTO.Id != 0)
            {
                // TODO
                var todoItem = await unitOfWork.TodoItems.GetByIdWithSubtasksAsync(updatedTodoItemDTO.Id);
                var ownerAvatar = await unitOfWork.Avatars.GetByIdAsync(1017);

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
                        var cycleInfo = todoItemsCyclesService.CreateCycleInfo((RepetitionPeriod)updatedTodoItemDTO.RepetitionPeriod.Value, updatedTodoItemDTO.StartDate.Value, updatedTodoItemDTO.EndDate.Value);
                        todoItem.TodoItemCycle = cycleInfo;
                        await unitOfWork.TodoItemsCycles.UpdateAsync(cycleInfo);
                    }
                    // Repetition removed
                    else
                    {
                        unitOfWork.TodoItemsCycles.Remove(todoItem.TodoItemCycle);
                    }
                }

                var todoItemModifiedEvent = await eventItemsService.CreateEventItem(
                    ownerAvatar.Id, 
                    EventType.TodoItemModified, 
                    EntityType.TodoItem, 
                    todoItem.Id);

                await unitOfWork.CommitAsync();

                //notificationsService.AddNotification(NotificationsHelper.CreateNotification(todoItem.Avatar, todoItemModifiedEvent, todoItem));
                //await notificationsService.SendAllNotifications();
            }
        }

        public async Task CompleteTodoItem(TodoItemDTO todoItemDTO)
        {
            if (todoItemDTO != null && todoItemDTO.Id != 0)
            {
                var todoItem = await unitOfWork.TodoItems.GetByIdAsync(todoItemDTO.Id);
                var avatar = await unitOfWork.Avatars.GetByIdAsync(todoItemDTO.AvatarId);

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
                var todoItemCompletedEvent = await eventItemsService.CreateEventItem(
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

                var avatarDto = mapper.Map<AvatarDTO>(avatar);
                if(avatarDto != null)
                    await notificationsService.UpdateAvatar(avatarDto);
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

            var todoItemDeletedEvent = await eventItemsService.CreateEventItem(
                todoItem.UserId,
                EventType.TodoItemDeleted,
                EntityType.TodoItem,
                todoItem.Id);

            // notificationsService.AddNotification(NotificationsHelper.CreateNotification(todoItem.Avatar, todoItemDeletedEvent, todoItem));
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
