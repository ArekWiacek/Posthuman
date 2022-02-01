namespace Posthuman.Core.Models.Enums
{
    public enum EventType
    {
        None = 0,

        UserRegistered = 1,

        AvatarCreated = 10,
        AvatarModified = 11,
        AvatarExpGained = 12,
        AvatarLevelGained = 13,

        TodoItemCreated = 20,
        TodoItemModified = 21,
        TodoItemDeleted = 22,
        TodoItemCompleted = 23,

        ProjectCreated = 30,
        ProjectModified = 31,
        ProjectDeleted = 32,
        ProjectFinished = 33,

        CardDiscovered = 40,

        TodoItemCyclicCreated = 100,
        TodoItemCyclicModified = 101,
        TodoItemCyclicDeleted = 102,
        TodoItemCyclicCompleted = 103
    }
}
