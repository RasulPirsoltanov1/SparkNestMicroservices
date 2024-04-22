//using SparkNest.Common.DTOs;
//using SparkNest.UI.MVC.Application.Abstractions;
//using SparkNest.UI.MVC.Application.DTOs.Blog;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http.Json;
//using System.Text;
//using System.Threading.Tasks;

//namespace SparkNest.UI.MVC.Application.Concretes
//{
//    public class BlogService : IBlogService
//    {
//        HttpClient _httpClient;
        
//        IFIleS
//        public BlogService(HttpClient httpClient)
//        {
//            _httpClient = httpClient;
//        }


//        public async Task<bool> CreateCategoryAsync(BlogCategoryCreateDTO blogCategoryCreateDTO)
//        {
//            try
//            {
//                var response = await _httpClient.PostAsJsonAsync("category", blogCategoryCreateDTO);
//                var result = await response.Content.ReadFromJsonAsync<Response<bool>>();
//                return result.Data;
//            }
//            catch (Exception ex)
//            {
//                return false;
//            }
//        }


//        public async Task<bool> CreateBlogAsync(BlogCreateDTO blogCreateDTO)
//        {
//            try
//            {
//                if (blogCreateDTO.Photo is not null)
//                {

//                }

//                var response = await _httpClient.PostAsJsonAsync("blog", blogCreateDTO);
//                var result = await response.Content.ReadFromJsonAsync<Response<bool>>();
//                return result.Data;
//            }
//            catch (Exception ex)
//            {
//                return false;
//            }
//        }

//        public async Task<bool> DeleteAsync(string blogId)
//        {
//            try
//            {
//                var response = await _httpClient.DeleteAsync($"blog/{blogId}");
//                var result = await response.Content.ReadFromJsonAsync<Response<bool>>();
//                return result.Data;
//            }
//            catch (Exception ex)
//            {
//                return false;
//            }
//        }

//        public async Task<List<BlogDTO>> GetAllBlogs()
//        {
//            try
//            {
//                var response = await _httpClient.GetAsync("blog");
//                var result = await response.Content.ReadFromJsonAsync<Response<List<BlogDTO>>>();
//                return result.Data;
//            }
//            catch (Exception ex)
//            {
//                return new List<BlogDTO>();
//            }
//        }

//        public async Task<BlogDTO> GetByBlogId(string blogId)
//        {
//            try
//            {
//                var response = await _httpClient.GetAsync($"blog/{blogId}");
//                var result = await response.Content.ReadFromJsonAsync<Response<BlogDTO>>();
//                return result.Data;
//            }
//            catch (Exception ex)
//            {
//                return new BlogDTO();
//            }
//        }

//        public async Task<List<BlogCategoryDTO>> GetAllCategories()
//        {
//            try
//            {
//                var response = await _httpClient.GetAsync("category");
//                var result = await response.Content.ReadFromJsonAsync<Response<List<BlogCategoryDTO>>>();
//                return result.Data;
//            }
//            catch (Exception ex)
//            {
//                return new List<BlogCategoryDTO>();
//            }
//        }

//        public async Task<BlogCategoryDTO> GetByCategoryId(string categoryId)
//        {
//            try
//            {
//                var response = await _httpClient.GetAsync($"category/{categoryId}");
//                var result = await response.Content.ReadFromJsonAsync<Response<BlogCategoryDTO>>();
//                return result.Data;
//            }
//            catch (Exception ex)
//            {
//                return new BlogCategoryDTO();
//            }
//        }
//    }
//}
