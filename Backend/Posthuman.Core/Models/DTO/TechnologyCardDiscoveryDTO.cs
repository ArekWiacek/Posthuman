using Posthuman.Core.Models.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Posthuman.Core.Models.DTO
{
    public class TechnologyCardDiscoveryDTO
    {
        public int Id { get; set; }
        public int AvatarId { get; set; }
        public bool HasBeenSeen { get; set; }
        // public Avatar Avatar { get; set; } = default!;
        public DateTime DiscoveryDate { get; set; }
        public int RewardCardId { get; set; }
        // public TechnologyCard RewardCard { get; set; } = default!;
    }
}
