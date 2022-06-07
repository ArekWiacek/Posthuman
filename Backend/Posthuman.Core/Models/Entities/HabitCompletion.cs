using System;
namespace Posthuman.Core.Models.Entities
{
    public class HabitCompletion
    {
        public HabitCompletion()
        {
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int HabitId { get; set; }
        public Habit Habit { get; set; }
        public DateTime Occured { get; set; }
    }
}
