
using KironTestWebAPI.Models.Entities;
using Microsoft.Extensions.Caching.Memory;
using static KironTestWebAPI.Models.Entities.CoinStats;

namespace KironTestWebAPI.CacheLayer
{
    public class CacheLayerX<T> : ICacheLayer<T>
    {
        public T GetCache(IMemoryCache cache, string key, T item)
        {
            item = cache.Get<T>(key);
            return item;
        }

        public T SetCache(IMemoryCache cache, string key, T item, TimeSpan timeSpan)
        {            
            return cache.Set(key, item, timeSpan);
        }
    }
}
