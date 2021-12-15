using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Posthuman.Core.Models.Entities
{
    [Table("TechnologyCardsDiscoveries")]
    public class TechnologyCardDiscovery
    {
        public int Id { get; set; }
        public int AvatarId { get; set; }
        public bool HasBeenSeen { get; set; }
        public Avatar Avatar { get; set; } = default!;
        public DateTime DiscoveryDate { get; set; }
        public int RewardCardId { get; set; }
        public TechnologyCard RewardCard { get; set; } = default!;
        public virtual ICollection<Requirement> Requirements { get; set; } = default!;
    }
}
