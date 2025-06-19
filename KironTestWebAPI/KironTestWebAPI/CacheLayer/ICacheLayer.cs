using Microsoft.Extensions.Caching.Memory;

namespace KironTestWebAPI.CacheLayer
{
    public interface ICacheLayer<T> //where T : class
    {
        T GetCache(IMemoryCache cache, string key, T item);
        T SetCache(IMemoryCache cache, string key, T item, TimeSpan timeSpan);
    }
}
