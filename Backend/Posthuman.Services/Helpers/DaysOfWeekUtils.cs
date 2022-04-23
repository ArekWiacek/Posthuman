using System;
using System.Collections.Generic;
using System.Linq;

namespace Posthuman.Services.Helpers
{
    public static class DaysOfWeekUtils
    {
        [Flags]
        private enum DayOfWeekBitFlag
        {
            None = 1 << 0,
            Monday = 1 << 1,
            Tuesday = 1 << 2,
            Wednesday = 1 << 3,
            Thursday = 1 << 4,
            Friday = 1 << 5,
            Saturday = 1 << 6,
            Sunday = 1 << 7
        }

        private static Dictionary<DayOfWeekBitFlag, string> daysTagNames;
        private static Dictionary<DayOfWeekBitFlag, DayOfWeek> systemDayOfWeek;
        private static Dictionary<DayOfWeek, DayOfWeekBitFlag> bitFlagDayOfWeek;

        static DaysOfWeekUtils()
        {
            systemDayOfWeek = new Dictionary<DayOfWeekBitFlag, DayOfWeek>()
            {
                { DayOfWeekBitFlag.Sunday,      DayOfWeek.Sunday },
                { DayOfWeekBitFlag.Monday,      DayOfWeek.Monday },
                { DayOfWeekBitFlag.Tuesday,     DayOfWeek.Tuesday },
                { DayOfWeekBitFlag.Wednesday,   DayOfWeek.Wednesday },
                { DayOfWeekBitFlag.Thursday,    DayOfWeek.Thursday },
                { DayOfWeekBitFlag.Friday,      DayOfWeek.Friday },
                { DayOfWeekBitFlag.Saturday,    DayOfWeek.Saturday }
            };

            bitFlagDayOfWeek = new Dictionary<DayOfWeek, DayOfWeekBitFlag>
            {
                { DayOfWeek.Sunday,     DayOfWeekBitFlag.Sunday },
                { DayOfWeek.Monday,     DayOfWeekBitFlag.Monday },
                { DayOfWeek.Tuesday,    DayOfWeekBitFlag.Tuesday },
                { DayOfWeek.Wednesday,  DayOfWeekBitFlag.Wednesday },
                { DayOfWeek.Thursday,   DayOfWeekBitFlag.Thursday },
                { DayOfWeek.Friday,     DayOfWeekBitFlag.Friday },
                { DayOfWeek.Saturday,   DayOfWeekBitFlag.Saturday },
            };

            daysTagNames = new Dictionary<DayOfWeekBitFlag, string> 
            {
                { DayOfWeekBitFlag.Sunday,      "sun" },
                { DayOfWeekBitFlag.Monday,      "mon" },
                { DayOfWeekBitFlag.Tuesday,     "tue" },
                { DayOfWeekBitFlag.Wednesday,   "wed" },
                { DayOfWeekBitFlag.Thursday,    "thu" },
                { DayOfWeekBitFlag.Friday,      "fri" },
                { DayOfWeekBitFlag.Saturday,    "sat" }
            };

            MondayToFridayCode = ValueOf(DayOfWeek.Monday, DayOfWeek.Tuesday,
                DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday);
        }

        public static int MondayToFridayCode { get; private set; }

        public static IEnumerable<DayOfWeek> EachDayOfTheWeek
        {
            get
            {
                yield return DayOfWeek.Sunday;
                yield return DayOfWeek.Monday;
                yield return DayOfWeek.Tuesday;
                yield return DayOfWeek.Wednesday;
                yield return DayOfWeek.Thursday;
                yield return DayOfWeek.Friday;
                yield return DayOfWeek.Saturday;
            }
        }

        public static IEnumerable<DayOfWeek> MondayToFriday
        {
            get 
            {
                return DaysOfWeek(MondayToFridayCode);
            }
        }

        public static IEnumerable<DayOfWeek> DaysOfWeek(int value)
        {
            foreach (DayOfWeekBitFlag bitFlag in systemDayOfWeek.Keys)
                if ((value & (int)bitFlag) == (int)bitFlag)
                    yield return systemDayOfWeek[bitFlag];
        }

        public static IEnumerable<string> DaysOfWeekTagNames(int value)
        {
            foreach (DayOfWeekBitFlag bitFlag in systemDayOfWeek.Keys)
                if ((value & (int)bitFlag) == (int)bitFlag)
                    yield return systemDayOfWeek[bitFlag].ToString().ToLower().Substring(0, 3);
        }

        /// <summary>
        /// Converts array of day names tags like ["mon", "wed", "sat"] and converts it to bitwise integer
        /// This is made to easily store collection of days as single number
        /// </summary>
        public static int ValueOf(string[] daysOfWeekTags)
        {
            var daysBitwise = DayOfWeekBitFlag.None;

            daysOfWeekTags.ToList().ForEach(dayOfWeekTag =>
            {
                var dayOfWeek = daysTagNames.FirstOrDefault(d => d.Value == dayOfWeekTag).Key;
                daysBitwise |= dayOfWeek;
            });

            return (int)daysBitwise;
        }

        public static int ValueOf(params DayOfWeek[] daysOfWeek)
        {
            var value = daysOfWeek.Distinct().Sum(dayOfWeek => (int)bitFlagDayOfWeek[dayOfWeek]);
            return value;
        }

    }
}
