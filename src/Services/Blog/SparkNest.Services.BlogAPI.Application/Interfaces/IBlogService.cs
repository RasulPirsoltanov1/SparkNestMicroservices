using SparkNest.Common.DTOs;
using SparkNest.Services.BlogAPI.Application.DTOs;
using SparkNest.Services.BlogAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.Services.BlogAPI.Application.Interfaces
{
    public interface IBlogService
    {
        Task<Response<bool>> CreateAsync(Blog blog);
        Task<Response<bool>> DeleteAsync(string blogId);
        Task<Response<bool>> DeletePhotoAsync(string blogId, string photoUrl);
        Task<Response<List<BlogDTO>>> GetAllAsync();
        Task<Response<BlogDTO>> GetByIdAsync(string blogId);
        Task<Response<NoContent>> UpdateAsync(BlogUpdateDTO blogUpdateDTO);
    }
}
