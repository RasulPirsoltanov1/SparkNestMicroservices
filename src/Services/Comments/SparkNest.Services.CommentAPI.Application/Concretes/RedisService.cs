using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SparkNest.Services.CommentAPI.Application.Concretes
{
    public class RedisService<T> : Abstractions.IRedisService<T> where T : class, new()
    {
        IDistributedCache _distributedCache;

        public RedisService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<T> GetStringAsync(string key)
        {
            try
            {
                var cache = await _distributedCache.GetStringAsync(key);
                var data = JsonSerializer.Deserialize<T>(cache);
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> SaveStringAsync(string key, string value)
        {
            try
            {
                await _distributedCache.SetStringAsync(key, value);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
