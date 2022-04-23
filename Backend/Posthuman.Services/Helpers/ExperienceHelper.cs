using System;
using Posthuman.Core.Models.Entities;
using Posthuman.Core.Models.Enums;

namespace Posthuman.Services.Helpers
{
    /// <summary>
    /// This class is responsible for calculating user development / progress.
    /// It provides methods to calculate experience points gained after different actions
    /// It provides info if user has enough XP to reach new level
    /// It holds part of game logic - for example how much xp user can get for doing stuff and so on
    /// It's also responsibe for a randomization mechanics, so game has degree of uncertanity and randomness
    /// </summary>
    public class ExperienceHelper : IExperienceHelper
    {
        private readonly Random random = new Random();

        public ExperienceHelper()
        {
            random = new Random(ThrowDices(3, 666));
        }

        /// <summary>
        /// Returns how much XP points for particular event
        /// </summary>
        public int CalculateExperienceForEvent(EventItem eventItem)
        {
            var eventType = eventItem.Type;
            var baseXp = GetBaseXpForEventType(eventType);
            var dicerollXp = GetDicerollXpForEventType(eventType);

            // Randomize multiplier so final XP is multiplied by 85 - 120 %
            float randomMultiplier = ((float)random.Next(85, 120)) / 100;
            
            return Convert.ToInt32((baseXp + dicerollXp) * randomMultiplier);
        }

        /// <summary>
        /// Returns ExperienceRange for given level - at what ammount of XP given level starts, 
        /// and when it ends (if user will reach or exceed it, then he reaches new level)
        /// Example: 
        ///     You reach Level 2 when you have at least 100 XP, and Level 2 ends at 250 XP (then level 3 starts)
        ///     So calling GetExperienceRangeForLevel(int level) will return range of 100 and 250
        /// </summary>
        public ExperienceRange GetXpRangeForLevel(int level)
        {
            // TODO more levels
            return GameLogicConstants.ExpRangeForLevel.ContainsKey(level) ? GameLogicConstants.ExpRangeForLevel[level] : new ExperienceRange(0, 0);
        }

        private int GetBaseXpForEventType(EventType type)
        {
            return GameLogicConstants.BaseExpForEvents.ContainsKey(type) ? GameLogicConstants.BaseExpForEvents[type] : 0;
        }

        private int GetDicerollXpForEventType(EventType eventType)
        {
            if (GameLogicConstants.DicerollExpForEvents.ContainsKey(eventType))
            {
                var dicerollParams = GameLogicConstants.DicerollExpForEvents[eventType];
                return ThrowDices(dicerollParams.Item1, dicerollParams.Item2);
            }

            return 0;
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
            for (var throwedDices = 0; throwedDices < howManyThrows; throwedDices++)
            {
                result += random.Next(1, diceWallCount);
            }

            return result;
        }
    }
}
