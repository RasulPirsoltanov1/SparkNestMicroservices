using SparkNest.Common.DTOs;
using SparkNest.UI.MVC.Application.Abstractions;
using SparkNest.UI.MVC.Application.DTOs.Blog;
using SparkNest.UI.MVC.Helpers;
using SparkNest.UI.MVC.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.UI.MVC.Application.Concretes
{
    public class BlogService : IBlogService
    {
        HttpClient _httpClient;

        IFileStockService _fileStockService;
        FileStockHelper _fileStockHelper;
        public BlogService(HttpClient httpClient, IFileStockService fileStockService, FileStockHelper fileStockHelper)
        {
            _httpClient = httpClient;
            _fileStockService = fileStockService;
            _fileStockHelper = fileStockHelper;
        }


        public async Task<bool> CreateCategoryAsync(BlogCategoryCreateDTO blogCategoryCreateDTO)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("category", blogCategoryCreateDTO);
                var result = await response.Content.ReadFromJsonAsync<Response<bool>>();
                return result.Data;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<bool> CreateBlogAsync(BlogCreateDTO blogCreateDTO)
        {
            try
            {
                if (blogCreateDTO.Photo is not null)
                {
                    var photoUrl = await _fileStockService.UploadPhoto(blogCreateDTO.Photo);
                    blogCreateDTO.PhotoUrl = photoUrl.Url;
                }

                var response = await _httpClient.PostAsJsonAsync("blog", blogCreateDTO);
                var result = await response.Content.ReadFromJsonAsync<Response<bool>>();
                return result.Data;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(string blogId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"blog/{blogId}");
                var result = await response.Content.ReadFromJsonAsync<Response<bool>>();
                return result.Data;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<BlogDTO>> GetAllBlogs()
        {
            try
            {
                var response = await _httpClient.GetAsync("blog");
                var result = await response.Content.ReadFromJsonAsync<Response<List<BlogDTO>>>();
                result?.Data.ForEach(x =>
                {
                    x.PhotoFileStockUrl = _fileStockHelper.GetFileStockUrl(x?.PhotoUrl);
                });
                return result.Data;
            }
            catch (Exception ex)
            {
                return new List<BlogDTO>();
            }
        }

        public async Task<BlogDTO> GetByBlogId(string blogId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"blog/{blogId}");
                var result = await response.Content.ReadFromJsonAsync<Response<BlogDTO>>();
                result.Data.PhotoFileStockUrl = _fileStockHelper.GetFileStockUrl(result.Data?.PhotoUrl);
                return result.Data;
            }
            catch (Exception ex)
            {
                return new BlogDTO();
            }
        }

        public async Task<List<BlogCategoryDTO>> GetAllCategories()
        {
            try
            {
                var response = await _httpClient.GetAsync("category");
                var result = await response.Content.ReadFromJsonAsync<Response<List<BlogCategoryDTO>>>();
                return result.Data;
            }
            catch (Exception ex)
            {
                return new List<BlogCategoryDTO>();
            }
        }

        public async Task<BlogCategoryDTO> GetByCategoryId(string categoryId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"category/{categoryId}");
                var result = await response.Content.ReadFromJsonAsync<Response<BlogCategoryDTO>>();
                return result.Data;
            }
            catch (Exception ex)
            {
                return new BlogCategoryDTO();
            }
        }

        public async Task<bool> UpdateBlogAsync(BlogUpdateDTO blogUpdateDTO)
        {
            try
            {
                if (blogUpdateDTO.Photo is not null)
                {
                    var photoUrl = await _fileStockService.UploadPhoto(blogUpdateDTO.Photo);
                    blogUpdateDTO.PhotoUrl = photoUrl.Url;
                }
                var response = await _httpClient.PutAsJsonAsync("blog",blogUpdateDTO);
                var result = await response.Content.ReadFromJsonAsync<Response<bool>>();
                return result.Data;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteCategoryAsync(string categoryId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"category/{categoryId}");
                var result = await response.Content.ReadFromJsonAsync<Response<bool>>();
                return result.Data;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> SubscibeAsync(string email)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"subscriber",new
                {
                    Email = email
                });
                var result = await response.Content.ReadFromJsonAsync<Response<bool>>();
                return result.Data;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
