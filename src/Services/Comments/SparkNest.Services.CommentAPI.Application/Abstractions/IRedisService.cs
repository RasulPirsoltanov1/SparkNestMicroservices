using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.Services.CommentAPI.Application.Abstractions
{
    public interface IRedisService<T>
    {
        Task<bool> SaveStringAsync(string key, string value);
        Task<T> GetStringAsync(string key);
    }
}
