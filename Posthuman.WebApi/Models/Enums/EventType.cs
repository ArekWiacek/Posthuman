namespace PosthumanWebApi.Models.Enums
{
    public enum EventType
    {
        None = 0,

        TodoItemCreated = 1,
        TodoItemModified = 2,
        TodoItemDeleted = 3,
        TodoItemCompleted = 4,

        ProjectCreated = 5,
        ProjectModified = 6,
        ProjectDeleted = 7,
        ProjectFinished = 8
    }
}
