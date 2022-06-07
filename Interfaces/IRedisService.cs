using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Interfaces
{
    public interface IRedisService
    {
        Task<T> Get<T>(string cacheKey);
        Task<T> Set<T>(string cacheKey, T value);
        Task Delete(string cacheKey);
    }
}
