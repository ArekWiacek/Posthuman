/*
 * using PosthumanWebApi.Models;
using PosthumanWebApi.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PosthumanWebApi.Repositories
{
    public interface ITodoItemsRepository
    {
        void Add(TodoItem item);
        IEnumerable<TodoItem> GetAll();
        TodoItem Find(long id);
        void Remove(long id);
        void Update(TodoItem item);

    }

    public class TodoItemsRepository : ITodoItemsRepository
    {
        private readonly PosthumanContext context;

        public TodoItemsRepository(PosthumanContext context)
        {
            this.context = context;
        }

        public void Add(TodoItem item)
        {
            context.TodoItems.Add(item);
            context.SaveChanges();
        }

        public TodoItem Find(long id)
        {
            return context.TodoItems.FirstOrDefault(item => item.Id == id);
        }

        public IEnumerable<TodoItem> GetAll()
        {
            return context.TodoItems.ToList();
        }

        public void Remove(long id)
        {
            var item = context.TodoItems.First(item => item.Id == id);
            context.TodoItems.Remove(item);
            context.SaveChanges();
        }

        public void Update(TodoItem item)
        {
            context.TodoItems.Update(item);
            context.SaveChanges();
        }
    }
}
*/