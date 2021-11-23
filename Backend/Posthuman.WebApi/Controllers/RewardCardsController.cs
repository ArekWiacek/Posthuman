using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Posthuman.Core.Models.DTO;
using Posthuman.Core.Models.Entities;
using Posthuman.Core.Services;

namespace PosthumanWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RewardCardsController : ControllerBase
    {
        private readonly ILogger<RewardCardsController> logger;
        private readonly IRewardCardsService rewardCardsService;

        public RewardCardsController(
            ILogger<RewardCardsController> logger,
            IRewardCardsService rewardCardsService)
        {
            this.logger = logger;
            this.rewardCardsService = rewardCardsService;
        }


        // GET: api/RewardCard/5
        [HttpGet("{avatarId}")]
        public async Task<ActionResult<IEnumerable<RewardCardDTO>>> GetRewardCardForAvatar(int avatarId)
        {
            var cards = await rewardCardsService.GetRewardCardsForAvatar(avatarId);

            return Ok(cards);
        }
    }
}
