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

        public TodoItemsService(
            IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        async Task<TodoItemDTO> GetTodoItemById(int id)
        {
            var todoItem = await unitOfWork.TodoItems.GetByIdAsync(id);

            if (todoItem != null)
                return TodoItemToDTO(todoItem);

            return null;
        }

        async Task<TodoItemDTO> ITodoItemsService.GetTodoItemById(int id)
        {
            var todoItem = await unitOfWork.TodoItems.GetByIdAsync(id);

            if (todoItem != null)
                return TodoItemToDTO(todoItem);

            return null;

        }
        
        public async Task<TodoItemDTO> CreateTodoItem(TodoItemDTO newTodoItemDTO)
        {
            var newTodoItem = new TodoItem(
                0,                                  // ID generated automatically
                newTodoItemDTO.Title,
                newTodoItemDTO.Description,
                false,                              // IsCompleted always false at the beginning
                newTodoItemDTO.Deadline,
                newTodoItemDTO.ProjectId);

            newTodoItem.CreationDate = DateTime.Now;

            // New TodoItem has parent Project selected - so update subtasks counter 
            if(newTodoItem.ProjectId != null && newTodoItem.ProjectId.Value != 0)
            {
                var parentProject = await unitOfWork.Projects.GetByIdAsync(newTodoItem.ProjectId.Value);
                if(parentProject != null)
                {
                    parentProject.TotalSubtasks += 1;
                }
            }

            await unitOfWork.TodoItems.AddAsync(newTodoItem);

            var todoItemCreatedEvent = new EventItem(2, EventType.TodoItemCreated, DateTime.Now, EntityType.TodoItem, newTodoItem.Id);
            await unitOfWork.EventItems.AddAsync(todoItemCreatedEvent);

            var avatar = await unitOfWork.Avatars.GetByIdAsync(2);
            if(avatar != null)
            {
                avatar.Exp += todoItemCreatedEvent.ExpGained;
            }

            await unitOfWork.CommitAsync();

            return TodoItemToDTO(newTodoItem);
        }

        public async Task DeleteTodoItem(int id)
        {
            var todoItem = await unitOfWork.TodoItems.GetByIdAsync(id);

            await DeleteTodoItem(todoItem);
        }

        public async Task DeleteTodoItem(TodoItem todoItem)
        {
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

            var todoItemDeletedEvent = new EventItem(2, EventType.TodoItemDeleted, DateTime.Now, EntityType.TodoItem, todoItem.Id);
            await unitOfWork.EventItems.AddAsync(todoItemDeletedEvent);

            // Update (decrease) Avatar's Exp points
            var avatar = await unitOfWork.Avatars.GetByIdAsync(2);
            if (avatar != null)
                avatar.Exp += todoItemDeletedEvent.ExpGained;

            await unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<TodoItemDTO>> GetAllTodoItems()
        {
            var allTodoItems = await unitOfWork.TodoItems.GetAllAsync();
            
            return allTodoItems
                .Select(todoItem => TodoItemToDTO(todoItem))
                .ToList();
        }

        public Task<IEnumerable<TodoItem>> GetByParentProject(int projectId)
        {
            throw new NotImplementedException();
        }


        public async Task UpdateTodoItem(TodoItemDTO todoItemDTO)
        {
            if(todoItemDTO != null && todoItemDTO.ProjectId.HasValue)
            {
                var todoItem = await unitOfWork.TodoItems.GetByIdAsync(todoItemDTO.ProjectId.Value);

                if (todoItem != null)
                {
                    todoItem.Title = todoItemDTO.Title;
                    todoItem.Description = todoItemDTO.Description;
                    todoItem.Deadline = todoItemDTO.Deadline;

                    // Parent Project was changed - update both old and new parent
                    if (todoItem.ProjectId != todoItemDTO.ProjectId)
                    {
                        var oldParentProject = unitOfWork.Projects.GetByIdAsync(todoItem.ProjectId.Value);
                        var newParentProject = unitOfWork.Projects.GetByIdAsync(todoItemDTO.ProjectId.Value);

                        if (oldParentProject != null)
                            oldParentProject.Result.TotalSubtasks--;

                        if(newParentProject != null)
                            newParentProject.Result.TotalSubtasks++;

                        todoItem.ProjectId = todoItemDTO.ProjectId;
                    }

                    var todoItemModifiedEvent = new EventItem(2, EventType.TodoItemModified, DateTime.Now, EntityType.TodoItem, todoItem.Id);
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
                        var todoItemCompletedEvent = new EventItem(2, EventType.TodoItemCompleted, DateTime.Now, EntityType.TodoItem, todoItem.Id);
                        await unitOfWork.EventItems.AddAsync(todoItemCompletedEvent);

                        // Update Avatar Exp points
                        var avatar = await unitOfWork.Avatars.GetByIdAsync(2);
                        if (avatar != null)
                            avatar.Exp += todoItemCompletedEvent.ExpGained;
                    }

                    await unitOfWork.CommitAsync();
                }
            }
        }

        ///private bool TodoItemExists(int id) =>
        //    _context.TodoItems.Any(todoItem => todoItem.Id == id);

        private static TodoItemDTO TodoItemToDTO(TodoItem todoItem) =>
            new TodoItemDTO(
                todoItem.Id,
                todoItem.Title,
                todoItem.Description,
                todoItem.IsCompleted,
                todoItem.Deadline,
                todoItem.ProjectId);
    }
}
