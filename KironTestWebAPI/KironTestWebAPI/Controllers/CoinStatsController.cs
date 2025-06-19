using KironTestWebAPI.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Drawing;
using static KironTestWebAPI.Models.Entities.CoinStats;
using static KironTestWebAPI.Components.CoinStatsComponent;
using KironTestWebAPI.Components;
using KironTestWebAPI.CacheLayer;

namespace KironTestWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CoinStatsController : ControllerBase
    {
        private readonly IMemoryCache cache;
        public CoinStatsController(IMemoryCache cache)
        {
            this.cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> GetData()
        {
            CacheLayerX<Root> cacheLayer = new CacheLayerX<Root>();
            CoinStatsComponent coinStats = new CoinStatsComponent();
            Root root = new Root();

            Root coinCache = cacheLayer.GetCache(cache, "CNS", root);
            if (coinCache == null)
            {
                coinCache = await coinStats.GetCoinStatsAsync();
                if (coinCache == null)
                {
                    return NotFound();
                }

                root = cacheLayer.SetCache(cache, "CNS", coinCache, TimeSpan.FromMinutes(60));
                return Ok(root);
            }

            return Ok(coinCache);
        }
    }
}
