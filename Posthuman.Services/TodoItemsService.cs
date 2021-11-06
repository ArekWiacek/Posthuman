using AutoMapper;
using Posthuman.Core;
using Posthuman.Core.Models.DTO;
using Posthuman.Core.Models.Entities;
using Posthuman.Core.Models.Enums;
using Posthuman.Core.Services;

namespace Posthuman.Services
{
    public class TodoItemsService : ITodoItemsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TodoItemsService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<TodoItemDTO> GetTodoItemById(int id)
        {
            var todoItem = await unitOfWork.TodoItems.GetByIdAsync(id);

            return mapper.Map<TodoItemDTO>(todoItem);
        }
        
        public async Task<TodoItemDTO> CreateTodoItem(TodoItemDTO newTodoItemDTO)
        {
            var newTodoItem = mapper.Map<TodoItem>(newTodoItemDTO);
            newTodoItem.IsCompleted = false;            // This always false when creating
            newTodoItem.CreationDate = DateTime.Now;    // This is set by application

            if (newTodoItem == null)
                throw new Exception();

            // Set owner Avatar
            var ownerAvatar = await 
                unitOfWork
                .Avatars
                .GetByIdAsync(newTodoItemDTO.AvatarId);

            if (ownerAvatar != null)
                newTodoItem.Avatar = ownerAvatar;

            // Has parent todo item?
            if(newTodoItem != null && newTodoItem.ParentId.HasValue)
            {
                var parentTodoItem = await unitOfWork.TodoItems.GetByIdAsync(newTodoItem.ParentId.Value);
                if (parentTodoItem == null)
                    throw new Exception();

                newTodoItem.Parent = parentTodoItem;
            }

            // New TodoItem has parent Project selected - so update subtasks counter 
            if(newTodoItem.ProjectId != null && newTodoItem.ProjectId.Value != 0)
            {
                var parentProject = await 
                    unitOfWork
                    .Projects
                    .GetByIdAsync(newTodoItem.ProjectId.Value);

                if(parentProject != null)
                {
                    parentProject.TotalSubtasks += 1;
                }
            }

            await unitOfWork.TodoItems.AddAsync(newTodoItem);
            await unitOfWork.CommitAsync();

            var todoItemCreatedEvent = new EventItem(
                ownerAvatar.Id, 
                EventType.TodoItemCreated, 
                DateTime.Now, 
                EntityType.TodoItem, 
                newTodoItem.Id);

            await unitOfWork.EventItems.AddAsync(todoItemCreatedEvent);

            ownerAvatar.Exp += todoItemCreatedEvent.ExpGained;

            await unitOfWork.CommitAsync();

            return mapper.Map<TodoItemDTO>(newTodoItem);
        }

        public async Task DeleteTodoItem(int id)
        {
            var todoItem = await unitOfWork.TodoItems.GetByIdAsync(id);

            var todoItemWithSubtasks = await unitOfWork.TodoItems.GetByIdWithSubtasksAsync(todoItem.AvatarId, todoItem.Id);

            if (todoItem == null)
                return; // NotFound()


            if (todoItem.ProjectId != null)
            {
                var parentProject = await unitOfWork.Projects.GetByIdAsync(todoItem.ProjectId.Value);

                if (parentProject != null)
                {
                    parentProject.TotalSubtasks--;
                }
            }

            await DeleteTodoItemWithSubtasks(todoItem);

            await unitOfWork.CommitAsync();
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

            var todoItemDeletedEvent = new EventItem(
                todoItem.AvatarId,
                EventType.TodoItemDeleted,
                DateTime.Now,
                EntityType.TodoItem,
                todoItem.Id);

            await unitOfWork.EventItems.AddAsync(todoItemDeletedEvent);

            await UpdateAvatarExp(todoItemDeletedEvent);
        }

        public async Task UpdateAvatarExp(EventItem eventItem)
        {
            // Update Avatar's Exp points based on what happened
            var avatar = await unitOfWork.Avatars.GetByIdAsync(eventItem.AvatarId);
            if (avatar != null)
                avatar.Exp += eventItem.ExpGained;
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

            var allTodoItems = await
                unitOfWork
                .TodoItems
                .GetAllByAvatarId(avatar.Id);

            var allTodoItemsMapped =
                mapper.Map<IEnumerable<TodoItemDTO>>(allTodoItems);

            return allTodoItemsMapped.ToList();
        }

        public async Task<IEnumerable<TodoItemDTO>> GetTodoItemsHierarchical()
        {
            var avatar = await unitOfWork.Avatars.GetActiveAvatarAsync();

            var todoItems = await
                unitOfWork
                .TodoItems
                .GetAllByAvatarId(avatar.Id);

            var topLevelTasks = todoItems.Where(ti => ti.IsTopLevel()).ToList();
            var flattenedTasksList = await FlattenSubtasksListAsync(topLevelTasks);

            var itemsMapped =
                mapper.Map<IEnumerable<TodoItemDTO>>(flattenedTasksList);

            return itemsMapped.ToList();
        }

        public async Task UpdateTodoItem(TodoItemDTO todoItemDTO)
        {
            if(todoItemDTO != null && todoItemDTO.Id != 0)
            {
                var todoItem = await unitOfWork.TodoItems.GetByIdAsync(todoItemDTO.Id);

                if (todoItem != null)
                {
                    todoItem.Title = todoItemDTO.Title;
                    todoItem.Description = todoItemDTO.Description;
                    todoItem.Deadline = todoItemDTO.Deadline;

                    // Parent Project was changed - update both old and new parent
                    if (todoItem.ProjectId != todoItemDTO.ProjectId)
                    {
                        if (todoItem.ProjectId.HasValue)
                        {
                            var oldParentProject = await unitOfWork.Projects.GetByIdAsync(todoItem.ProjectId.Value);
                            if (oldParentProject != null)
                                oldParentProject.TotalSubtasks--;
                        }

                        if (todoItemDTO.ProjectId.HasValue)
                        {
                            var newParentProject = await unitOfWork.Projects.GetByIdAsync(todoItemDTO.ProjectId.Value);

                            if (newParentProject != null)
                                newParentProject.TotalSubtasks++;
                        }

                        todoItem.ProjectId = todoItemDTO.ProjectId;
                    }

                    var ownerAvatar = await unitOfWork.Avatars.GetActiveAvatarAsync();
                    if(ownerAvatar == null)
                    {
                        throw new Exception($"Task owner (Avatar of ID: {ownerAvatar.Id}) could not be found");
                    }

                    // Assign parent todo item
                    if(todoItem.ParentId != todoItemDTO.ParentId && todoItemDTO.ParentId.HasValue)
                    {
                        var parentTask = await unitOfWork.TodoItems.GetByIdAsync(todoItemDTO.ParentId.Value);
                        todoItem.Parent = parentTask;
                    }

                    var todoItemModifiedEvent = new EventItem(
                        ownerAvatar.Id, 
                        EventType.TodoItemModified, 
                        DateTime.Now, 
                        EntityType.TodoItem, 
                        todoItem.Id);

                    await unitOfWork.EventItems.AddAsync(todoItemModifiedEvent);

                    // TodoItem was just completed
                    if (todoItem.IsCompleted == false && todoItemDTO.IsCompleted == true)
                    {
                        todoItem.CompletionDate = DateTime.Now;
                        todoItem.IsCompleted = todoItemDTO.IsCompleted;

                        // If TodoItem is subtask of a project, update CompletedSubtasks property
                        if (todoItem.ProjectId != null)
                        {
                            var parentProject = await unitOfWork.Projects.GetByIdAsync(todoItem.ProjectId.Value);

                            if (parentProject != null)
                                parentProject.CompletedSubtasks++;
                        }

                        // Add event of completion

                        var todoItemCompletedEvent = new EventItem(
                            ownerAvatar.Id, 
                            EventType.TodoItemCompleted, 
                            DateTime.Now, 
                            EntityType.TodoItem, 
                            todoItem.Id);
                        await unitOfWork.EventItems.AddAsync(todoItemCompletedEvent);

                        // Update Avatar Exp points
                        ownerAvatar.Exp += todoItemCompletedEvent.ExpGained;
                    }

                    await unitOfWork.CommitAsync();
                }
            }
        }


        private async Task<IEnumerable<TodoItem>> FlattenSubtasksListAsync(IEnumerable<TodoItem> tasks)
        {
            var newList = new List<TodoItem>();

            foreach (var task in tasks)
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
    }
}
