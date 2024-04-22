using SparkNest.UI.MVC.Application.DTOs;
using SparkNest.UI.MVC.Application.DTOs.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.UI.MVC.Application.Abstractions
{
    public interface IBlogService
    {
        Task<bool> CreateBlogAsync(BlogCreateDTO blogCreateDTO);
        Task<bool> UpdateBlogAsync(BlogUpdateDTO blogUpdateDTO);
        Task<bool> CreateCategoryAsync(BlogCategoryCreateDTO blogCategoryCreateDTO);
        Task<bool> DeleteAsync(string blogId);
        Task<bool> DeleteCategoryAsync(string categoryId);
        Task<List<BlogDTO>> GetAllBlogs();
        Task<List<BlogCategoryDTO>> GetAllCategories();
        Task<BlogDTO> GetByBlogId(string blogId);
        Task<BlogCategoryDTO> GetByCategoryId(string categoryId);
    }
}
