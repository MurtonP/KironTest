using Azure;
using KironTestWebAPI.Controllers;
using KironTestWebAPI.Models;
using KironTestWebAPI.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Identity.Client;
using System.Collections;
using System.Diagnostics.Eventing.Reader;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using static KironTestWebAPI.Models.Entities.CoinStats;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KironTestWebAPI.Components
{
    public class UKBankHolidaysComponent
    {
        //private readonly IMemoryCache cache;
        private static string? _bankHolidays;
        private readonly HttpClient _httpClient = new HttpClient(); 

        //public UKBankHolidaysComponent(IMemoryCache cache)
        //{
        //    this.cache = cache;
        //}

        public async Task<HolidayData> GetHolidayData(IMemoryCache cache)
        {
            HolidayData? days = cache.Get<HolidayData>("UBH");
            if (days != null)
            {
                return days;
            }

            var apiUrl = "https://www.gov.uk/bank-holidays.json";
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();

                HolidayData result = GetBankHolidays(data);

                cache.Set("UBH", result, TimeSpan.FromMinutes(30));

                return result;
            }
            else
            {
                // Handle the error condition
                return new HolidayData(); // StatusCode((int)response.StatusCode);
            }
        }

        public HolidayData GetBankHolidays(string receivedDays)
        {
            _bankHolidays = receivedDays;

            HolidayData holidays = BankHolidays();

            return holidays;
        }

        private HolidayData BankHolidays()
        {
            if (_bankHolidays != null)
            {
                HolidayData holidays = JsonSerializer.Deserialize<HolidayData>(_bankHolidays);

                return holidays;
            }
            else
            {
                return new HolidayData();
            }
        }
    }
}
