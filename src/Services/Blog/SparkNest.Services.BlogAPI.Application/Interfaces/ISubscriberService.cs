using SparkNest.Common.DTOs;
using SparkNest.Services.BlogAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.Services.BlogAPI.Application.Interfaces
{
    public interface ISubscriberService
    {
        Task<Response<bool>> CreateAsync(Subscriber subscriber);
        Task<Response<List<Subscriber>>> GetAllAsync();
        Task<Response<Subscriber>> GetByIdAsync(string blogId);
    }
}
