using Posthuman.Core.Models.Entities.Interfaces;
using System;

namespace Posthuman.Services.Helpers
{
    public static class ValidationHelper
    {
        public static void CheckIfExists(IEntity entity) 
        {
            var typeName = entity.GetType().Name;
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), $"Entity of type [{typeName}] is null.");

            if (entity.Id == 0)
                throw new ArgumentNullException(nameof(entity), $"Entity of type [{typeName}] has ID = 0.");
        }

        public static void CheckIfUserHasAccess(IOwnable ownableEntity, int userId)
        {
            if (ownableEntity.UserId != userId)
                throw new Exception($"Access denied: user with ID: {userId} is not owner of [{nameof(ownableEntity)}] entity.");
        }
    }
}
