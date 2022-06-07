using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Interfaces;

namespace TodoList.Services
{
    public class RedisService : IRedisService
    {
        private readonly IDistributedCache _cache;

        public RedisService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<T> Get<T>(string cacheKey)
        {
            var redisList = await _cache.GetStringAsync(cacheKey);
            if(redisList != null)
            {
                return JsonConvert.DeserializeObject<T>(redisList);
            }    
            return default;
        }

        public async Task<T> Set<T>(string cacheKey, T value)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
                SlidingExpiration = TimeSpan.FromMinutes(30)
            };
            //Set key value redis
            await _cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(value), options);
            return value;
        }

        public async Task Delete(string cacheKey)
        {
            await _cache.RemoveAsync(cacheKey);
        }
    }
}
