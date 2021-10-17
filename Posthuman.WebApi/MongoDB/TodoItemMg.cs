/*using MongoDB.Bson.Serialization.Attributes;

namespace PosthumanWebApi.Models.Entities
{
    /// <summary>
    /// Represents single "to-do" task
    /// </summary>
    public class TodoItemMg
    {
        public TodoItemMg(
            long id,
            string title,
            string description,
            bool isCompleted,
            DateTime? deadline)
        {
            //this.Id = id;
            this.Title = title;
            this.Description = description;
            this.IsCompleted = isCompleted;
            this.Deadline = deadline;
        }

        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? CompletionDate { get; set; }
    }
}
*/