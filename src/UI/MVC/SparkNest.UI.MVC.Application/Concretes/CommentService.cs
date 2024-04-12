using SparkNest.Common.DTOs;
using SparkNest.UI.MVC.Application.Abstractions;
using SparkNest.UI.MVC.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.UI.MVC.Infrastructure.Concretes
{
    public class CommentService : ICommentService
    {
        HttpClient _httpClient;

        public CommentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> Create(CreateCommentDTO createCommentDTO)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync("comments", createCommentDTO);
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<List<CreateCommentDTO>> GetAll()
        {
            try
            {
                var comments = await _httpClient.GetFromJsonAsync<Response<List<CreateCommentDTO>>>("comments");
                return comments.Data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<CreateCommentDTO>> GetAllByProductId(string productId)
        {
            try
            {
                var comments = await _httpClient.GetFromJsonAsync<Response<List<CreateCommentDTO>>>("comments");
                var currents = comments.Data.Where(x => x.ProductId == productId).ToList();
                return currents;
            }
            catch (Exception)
            {
                return new List<CreateCommentDTO>();
            }
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            try
            {
                var result = await _httpClient.DeleteAsync($"comments/{Id}");
                return result.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
