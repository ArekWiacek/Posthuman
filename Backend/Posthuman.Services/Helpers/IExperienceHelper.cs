using Posthuman.Core.Models.Entities;

namespace Posthuman.Services.Helpers
{
    public interface IExperienceHelper
    {
        int CalculateExperienceForEvent(EventItem eventItem);
        ExperienceRange GetXpRangeForLevel(int level);
    }
}