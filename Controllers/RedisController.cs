using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.DTO;
using TodoList.Entities;
using TodoList.Interfaces;

namespace TodoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ITodoItemService _todoItemService;
        private readonly IConfiguration _configuration;
        public RedisController(IDistributedCache distributedCache, ITodoItemService todoItemService, IConfiguration configuration)
        {
            _distributedCache = distributedCache;
            _todoItemService = todoItemService;
            _configuration = configuration;
        }
        [HttpGet("get")]
        public async Task<IActionResult> GetRedis()
        {
            var cacheKey = "todoItems";
            string serializedCustomerList;
            var todoDTOList = new List<TodoItemDTO>();

            var redisCustomerList = await _distributedCache.GetAsync(cacheKey);
            if (redisCustomerList != null)
            {
                serializedCustomerList = Encoding.UTF8.GetString(redisCustomerList);
                todoDTOList = JsonConvert.DeserializeObject<List<TodoItemDTO>>(serializedCustomerList);
            }

            return Ok(todoDTOList);
        }
        [HttpGet("get-keys")]
        public List<string> GetAllkeys()
        {
            List<string> listKeys = new List<string>();
            using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(_configuration.GetSection("Redis:PublicPoint").Value + ",allowAdmin=true"))
            {
                var keys = redis.GetServer(_configuration.GetSection("Redis:Host").Value, int.Parse(_configuration.GetSection("Redis:Port").Value)).Keys();
                listKeys.AddRange(keys.Select(key => (string)key).ToList());
            }

            return listKeys;
        }
        [HttpGet("remove-key")]
        public async Task<IActionResult> RemoveKey()
        {
            var cacheKey = "todoItems";
            await _distributedCache.RemoveAsync(cacheKey);
            return Ok();
        }
    }
}
