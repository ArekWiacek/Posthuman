using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Posthuman.Core.Models.DTO;
using Posthuman.Core.Models.Enums;
using Posthuman.Core.Services;

namespace PosthumanWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TechnologyCardsController : ControllerBase
    {
        private readonly ILogger<TechnologyCardsController> logger;
        private readonly ITechnologyCardsService technologyCardsService;

        public TechnologyCardsController(
            ILogger<TechnologyCardsController> logger,
            ITechnologyCardsService rewardCardsService)
        {
            this.logger = logger;
            this.technologyCardsService = rewardCardsService;
        }

        [HttpGet("{avatarId}")]
        public async Task<ActionResult<IEnumerable<TechnologyCardDTO>>> GetTechnologyCardsForAvatar(int avatarId)
        {
            var cards = await technologyCardsService.GetTechnologyCardsForAvatar(avatarId);
            return Ok(cards);
        }

        // GET: api/RewardCard/5
        [HttpGet("{avatarId}/{category}")]
        public async Task<ActionResult<IEnumerable<TechnologyCardDTO>>> GetTechnologyCardsOfCategory(int avatarId, int category)
        {
            IEnumerable<TechnologyCardDTO> cards = null;

            if (category == 1)
                cards = await technologyCardsService.GetTechnologyCardsForCategory(avatarId, CardCategory.Technology);
            else if (category == 2)
                cards = await technologyCardsService.GetTechnologyCardsForCategory(avatarId, CardCategory.Person);
            else
                return BadRequest("Invalid card category");

            return Ok(cards);
        }

        // GET: api/RewardCard/5
        [HttpGet("{avatarId}/discovery/{cardId}")]
        public async Task<ActionResult<TechnologyCardDiscoveryDTO>> DiscoverCard(int avatarId, int cardId)
        {
            var discovery = await technologyCardsService.DiscoverTechnologyCardForAvatar(avatarId, cardId);
            return Ok(discovery);
        }
    }
}
