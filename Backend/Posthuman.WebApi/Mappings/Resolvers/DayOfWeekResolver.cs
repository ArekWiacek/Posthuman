using AutoMapper;
using Posthuman.Core.Models.DTO.Habit;
using Posthuman.Core.Models.Entities;
using Posthuman.Services.Helpers;
using System.Linq;

namespace Posthuman.WebApi.Mappings.Resolvers
{
    public class ConvertSetOfDaysTagsToBitwiseInt : IValueResolver<CreateHabitDTO, Habit, int>
    {
        public int Resolve(CreateHabitDTO source, Habit destination, int destMember, ResolutionContext context)
        {
            var daysOfWeek = DaysOfWeekUtils.ValueOf(source.DaysOfWeek);
            return daysOfWeek;
        }
    }

    public class ConvertBitwiseNumberToSetOfDaysTags : IValueResolver<Habit, HabitDTO, string[]>
    {
        public string[] Resolve(Habit source, HabitDTO destination, string[] destMember, ResolutionContext context)
        {
            var tags = DaysOfWeekUtils.DaysOfWeekTagNames(source.DaysOfWeek);
            return tags.ToArray(); ;
        }
    }
}
