using Posthuman.Services.Helpers;
using System;
using System.Collections.Generic;

namespace Posthuman.WebApi.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime GetNearestDay(this DateTime date, IEnumerable<DayOfWeek> days)
        {
            return DateTime.Now;
        }

        public static DateTime GetNearestDay(this DateTime date, int daysBitFlag)
        {
            var days = DaysOfWeekUtils.DaysOfWeek(daysBitFlag);

            return DateTime.Now;
        }
    }
}
