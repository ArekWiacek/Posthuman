/*using MongoDB.Driver;
using PosthumanWebApi.Models.Entities;
using PosthumanWebApi.Models.Settings;

namespace PosthumanWebApi.Services
{
    public class PosthumanService
    {
        private readonly IMongoCollection<TodoItemMg> _todoItems;

        public PosthumanService(IPosthumanDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _todoItems = database.GetCollection<TodoItemMg>(settings.PosthumanCollectionName);
        }


        public List<TodoItemMg> Get() =>
            _todoItems.Find(todoItem => true).ToList();

        public TodoItemMg Get(string id) =>
            _todoItems.Find<TodoItemMg>(todoItem => todoItem.Id == id).FirstOrDefault();

        public TodoItemMg Create(TodoItemMg todoItem)
        {
            _todoItems.InsertOne(todoItem);
            return todoItem;
        }

        public void Update(string id, TodoItemMg todoItemIn) =>
            _todoItems.ReplaceOne(todoItem => todoItem.Id == id, todoItemIn);

        public void Remove(TodoItemMg todoItemIn) =>
            _todoItems.DeleteOne(todoItem => todoItem.Id == todoItemIn.Id);

        public void Remove(string id) =>
            _todoItems.DeleteOne(todoItem => todoItem.Id == id);
    }
}*/
