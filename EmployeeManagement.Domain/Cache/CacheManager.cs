using System;
using EmployeeManagement.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace EmployeeManagement.Domain.Cache
{
    public class CacheManager : ICacheManager
    {
        private readonly IMemoryCache _memoryCache;

        public CacheManager(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void Set(object key, object value)
        {
            _memoryCache.Set(key, value, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });
        }

        public void Remove(object key)
        {
            _memoryCache.Remove(key);
        }

        public bool TryGetValue(object key, object obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            return _memoryCache.TryGetValue(key, out obj);
        }

        public object Get(object key)
        {
            return _memoryCache.Get(key);
        }
    }
}
