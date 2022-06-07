using Posthuman.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Posthuman.Services.Helpers
{
    public static class GameLogicConstants
    {
        static GameLogicConstants()
        {
        }

        // Exp points for different events
        public static readonly Dictionary<EventType, int> BaseExpForEvents = new Dictionary<EventType, int>()
        {
            { EventType.None, 0 },

            { EventType.TodoItemCreated, 25 },
            { EventType.TodoItemDeleted, -25 },
            { EventType.TodoItemCompleted, 100 },

            { EventType.HabitCreated, 100 },
            { EventType.HabitDeleted, -100 },
            { EventType.HabitCompleted, 100 },

            {EventType.AvatarCreated, 20 }
        };

        public static readonly Dictionary<EventType, (int, int)> DicerollExpForEvents = new Dictionary<EventType, (int, int)>()
        {
            { EventType.TodoItemCreated, (1, 5) },
            { EventType.TodoItemCompleted, (2, 10) },

            { EventType.HabitCreated, (2, 5) },
            { EventType.HabitCompleted, (2, 10) }
        };

        // Exp range for different levels 
        public static readonly Dictionary<int, ExperienceRange> ExpRangeForLevel = new Dictionary<int, ExperienceRange>()
        {
            { 1, new ExperienceRange(0, 500) },                 // 500 
            { 2, new ExperienceRange(500, 1000) },              // 500
            { 3, new ExperienceRange(1000, 2200) },             // 700
            { 4, new ExperienceRange(2200, 3000) },             // 800
            { 5, new ExperienceRange(3000, 4000) },             // 1000
            { 6, new ExperienceRange(4000, 5200) },             // 1200
            { 7, new ExperienceRange(5200, 6500) },             // 1300
            { 8, new ExperienceRange(6500, 8000) },             // 1500
            { 9, new ExperienceRange(8000, 9700) },             // 1700
            { 10, new ExperienceRange(9700, 11500) }            // 1800
        };
    }
}
