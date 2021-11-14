namespace Posthuman.Core.Models.Enums
{
    public enum SubeventType
    {
        None = 0,

        TodoItemDescriptionAdded = 1,
        TodoItemDescriptionRemoved = 2,
        TodoItemDeadlineAdded = 3,
        TodoItemDeadlineRemoved = 4,
        TodoItemParentTaskAdded = 5,
        TodoItemParentTaskRemoved = 6,
        TodoItemProjectAdded = 7,
        TodoItemProjectRemoved = 8,

        //ProjectCreated = 5,
        //ProjectModified = 6,
        //ProjectDeleted = 7,
        //ProjectFinished = 8
    }
}
