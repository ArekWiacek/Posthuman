using AutoMapper;
using Posthuman.Core;
using Posthuman.Core.Models.DTO;
using Posthuman.Core.Models.Entities;
using Posthuman.Core.Models.Enums;
using Posthuman.Core.Repositories;
using Posthuman.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            var ownerAvatar = await unitOfWork.Avatars.GetActiveAvatarAsync();

            //if (todoItem.AvatarId != null != ownerAvatar.Id)
            //{
                // This todo item is owned by other Avatar - dont return it
            //    return null;
            //}

            return mapper.Map<TodoItemDTO>(todoItem);
        }
        
        public async Task<TodoItemDTO> CreateTodoItem(TodoItemDTO newTodoItemDTO)
        {
            var newTodoItem = mapper.Map<TodoItem>(newTodoItemDTO);
            newTodoItem.IsCompleted = false;            // This always false when creating
            newTodoItem.CreationDate = DateTime.Now;    // This is set by application

            // Set owner Avatar
            var ownerAvatar = await 
                unitOfWork
                .Avatars
                .GetByIdAsync(newTodoItemDTO.AvatarId);

            if (ownerAvatar != null)
                newTodoItem.Avatar = ownerAvatar;

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

            if (todoItem == null)
                return; // NotFound()

            unitOfWork.TodoItems.Remove(todoItem);

            if (todoItem.ProjectId != null)
            {
                var parentProject = await unitOfWork.Projects.GetByIdAsync(todoItem.ProjectId.Value);

                if (parentProject != null)
                {
                    parentProject.TotalSubtasks--;
                }
            }

            var todoItemDeletedEvent = new EventItem(
                todoItem.AvatarId, 
                EventType.TodoItemDeleted, 
                DateTime.Now, 
                EntityType.TodoItem, 
                todoItem.Id);

            await unitOfWork.EventItems.AddAsync(todoItemDeletedEvent);

            // Update (decrease) Avatar's Exp points
            var avatar = await unitOfWork.Avatars.GetByIdAsync(todoItem.AvatarId);
            if (avatar != null)
                avatar.Exp += todoItemDeletedEvent.ExpGained;

            await unitOfWork.CommitAsync();
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

        public Task<IEnumerable<TodoItem>> GetByParentProject(int projectId)
        {
            throw new NotImplementedException();
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
    }
}
