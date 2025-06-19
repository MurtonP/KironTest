using KironTestWebAPI.Models.Entities;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using static KironTestWebAPI.Models.Entities.CoinStats;

namespace KironTestWebAPI.Components
{
    public class CoinStatsComponent
    {
        private readonly HttpClient _httpClient;

        public CoinStatsComponent()
        {
            _httpClient = new HttpClient();
        }

        public async Task<Root> GetCoinStatsAsync()
        {
            _httpClient.BaseAddress = new Uri("https://openapiv1.coinstats.app");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "931cprx8hPI7sWrLD1dkBB5Y9hVdkC/GAR8KI5GWVAY=");
            HttpResponseMessage response = await _httpClient.GetAsync("/coins").ConfigureAwait(true);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                Root? coinStats = JsonConvert.DeserializeObject<Root>(data);

                return coinStats;
            }
            else
            {
                // Handle the error condition
                return new Root(); // StatusCode((int)response.StatusCode);

            }
        }
    }
}
