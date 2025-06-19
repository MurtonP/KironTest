using KironTestWebAPI.Components;
using KironTestWebAPI.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Text.Json;
using static KironTestWebAPI.Models.Entities.UKBankHolidays;

namespace KironTestWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UKBankHolidaysController : ControllerBase
    {
        private readonly IMemoryCache cache;
        public UKBankHolidaysController(IMemoryCache cache)
        {
            this.cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> GetData()
        {
            var result = new UKBankHolidaysComponent();
            HolidayData holidays = await result.GetHolidayData(cache);
            if (holidays is null)
            {
                return NotFound();
            }
            return Ok(holidays);
        }
    }
}
