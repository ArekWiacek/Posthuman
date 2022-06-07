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

        CardDiscovered = 30,

        HabitCreated = 40,
        HabitModified = 41,
        HabitDeleted = 42,
        HabitCompleted = 43
    }
}
