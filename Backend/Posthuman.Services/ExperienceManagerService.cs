using Posthuman.Core.Models.Entities;
using Posthuman.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace Posthuman.Services
{
    public class ExperienceManager
    {
        private readonly Random random = new Random();

        public ExperienceManager()
        {
            random = new Random(ThrowDices(3, 666));
        }

        /// <summary>
        /// Algorithm how XP is calculated
        /// 
        ///     Main XP based on EventType (what actually happened)         +
        ///     Additional XP based on SubeventType (details about event)   +   
        ///     One special throw of dices                                  
        ///     
        ///     multiplied by random number from 0.9 to 1.2 :)
        /// </summary>
        public int CalculateExperienceForEvent(EventItem eventItem, SubeventType? subeventType)
        {
            var eventType = eventItem.Type;
            var totalXpGained = BaseExpRewards[eventType];

            if (subeventType.HasValue)
                totalXpGained = SubeventExpRewards[subeventType.Value];

            // I darmowy rzut kostką od Aruszka
            switch (eventType)
            {
                case EventType.TodoItemCreated:
                    totalXpGained += ThrowDices(1, 2);
                    break;

                case EventType.TodoItemCompleted:
                    totalXpGained += ThrowDices(1, 6);
                    break;
            }

            // 90 - 120 %
            float randomMultiplier = ((float)random.Next(80, 130)) / 100;
            totalXpGained = Convert.ToInt32(totalXpGained * randomMultiplier);

            return totalXpGained;
        }

        /// <summary>
        /// This is my random number generator. You just throw the dices.
        /// Examples
        ///     1 throw, 6 wall-dice = number between 1 and 6
        ///     3 throw, 9 wall-dice = number between 3 and 27
        /// </summary>
        /// <param name="howManyThrows">How many dices you want to throw</param>
        /// <param name="diceWallCount">How many walls each dice has (normally - 6)</param>
        /// <returns>Random number</returns>
        private int ThrowDices(int howManyThrows, int diceWallCount = 6)
        {
            int result = 0;
            for (var throwedDices = 0; throwedDices == howManyThrows; throwedDices++)
                result += random.Next(1, diceWallCount);
            return result;
        }

        // Exp points for different events
        private Dictionary<EventType, int> BaseExpRewards = new Dictionary<EventType, int>()
        {
            { EventType.None, 0 },

            { EventType.TodoItemModified, 0 },
            { EventType.TodoItemCreated, 10 },
            { EventType.TodoItemDeleted, -10 },
            { EventType.TodoItemCompleted, 20 },

            // In future
            { EventType.ProjectCreated, 0 },
            { EventType.ProjectDeleted, 0 },
            { EventType.ProjectModified, 0 },
            { EventType.ProjectFinished, 0 }
        };

        // Exp points for minor details like editions etc
        private Dictionary<SubeventType, int> SubeventExpRewards = new Dictionary<SubeventType, int>()
        {
            { SubeventType.TodoItemDescriptionAdded, 2 },
            { SubeventType.TodoItemDeadlineAdded, 2 },
            { SubeventType.TodoItemParentTaskAdded, 4 },

            { SubeventType.TodoItemDescriptionRemoved, -2 },
            { SubeventType.TodoItemDeadlineRemoved, -2 },
            { SubeventType.TodoItemParentTaskRemoved, -4 },
            { SubeventType.TodoItemProjectAdded, 1 },
            { SubeventType.TodoItemProjectRemoved, -1 }
        };
    }
}
