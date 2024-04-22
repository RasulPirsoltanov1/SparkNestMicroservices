using MongoDB.Bson;
using MongoDB.Driver;
using SparkNest.Common.DTOs;
using SparkNest.Services.BlogAPI.Application.DTOs;
using SparkNest.Services.BlogAPI.Application.Interfaces;
using SparkNest.Services.BlogAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.Services.BlogAPI.Infrastructure.Concretes
{
    public class BlogService : IBlogService
    {
        IMongoDatabase _mongoDatabase;
        IMongoCollection<Blog> _blogCollection;
        IMongoCollection<Category> _categoryCollection;
        public BlogService(IMongoDatabase mongoDatabase)
        {
            this._mongoDatabase = mongoDatabase;
            _blogCollection = _mongoDatabase.GetCollection<Blog>(nameof(Blog));
            _categoryCollection = _mongoDatabase.GetCollection<Category>(nameof(Category));
        }

        public async Task<Response<bool>> CreateAsync(Blog blog)
        {
            try
            {
                await _blogCollection.InsertOneAsync(blog);
                return Response<bool>.Success(true, 200);
            }
            catch (Exception ex)
            {
                return Response<bool>.Fail(ex.Message, 500);
            }
        }

        public async Task<Response<bool>> DeleteAsync(string blogId)
        {
            try
            {
                var deleteFilter = Builders<Blog>.Filter.Eq(x => x.Id, blogId);
                await _blogCollection.DeleteOneAsync(deleteFilter);
                return Response<bool>.Success(true, 200);
            }
            catch (Exception ex)
            {
                return Response<bool>.Fail(ex.Message, 500);
            }
        }

        public Task<Response<bool>> DeletePhotoAsync(string blogId, string photoUrl)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<BlogDTO>>> GetAllAsync()
        {
            try
            {
                var blogs = await _blogCollection.Find(new BsonDocument()).ToListAsync();
                var blogDTOs = blogs.Select(blog => new BlogDTO
                {
                    Id = blog.Id,
                    Title = blog.Title,
                    Content = blog.Content,
                    CategoryId = blog.CategoryId,
                    CreateDate = blog.CreateDate,
                    PhotoUrl = blog.PhotoUrl,
                    Views = blog.Views,
                    UpdateDate = blog.UpdateDate
                }).ToList();
                foreach (var blog in blogDTOs)
                {
                    blog.Category = _categoryCollection.Find(x => x.Id == blog.CategoryId).FirstOrDefault();
                }
                return Response<List<BlogDTO>>.Success(blogDTOs, 200);
            }
            catch (Exception ex)
            {
                return Response<List<BlogDTO>>.Fail(ex.Message, 500);
            }
        }

        public async Task<Response<BlogDTO>> GetByIdAsync(string blogId)
        {
            try
            {
                var filter = Builders<Blog>.Filter.Eq(x => x.Id, blogId);
                var blog = await _blogCollection.Find(filter).FirstOrDefaultAsync();

                if (blog == null)
                    return Response<BlogDTO>.Fail("Blog not found", 404);
                if (blog.Views is null)
                {
                    blog.Views = 1;
                }
                else
                {
                    blog.Views += 1;
                }
                var blogUpdateDTO = new BlogDTO
                {

                    Id = blog.Id,
                    Title = blog.Title,
                    Content = blog.Content,
                    Category = blog.Category,
                    CategoryId = blog.CategoryId,
                    CreateDate = DateTime.Now,
                    PhotoUrl = blog.PhotoUrl,
                    UpdateDate = blog.UpdateDate,
                    Views = blog.Views,
                };

                return Response<BlogDTO>.Success(blogUpdateDTO, 200);
            }
            catch (Exception ex)
            {
                return Response<BlogDTO>.Fail(ex.Message, 500);
            }
        }


        public async Task<Response<NoContent>> UpdateAsync(BlogUpdateDTO blogUpdateDTO)
        {
            try
            {
                // Güncellenecek blogun ID'sini ObjectId türüne dönüştür

                // Blogu güncellemek için önce mevcut blogu al
                var filter = Builders<Blog>.Filter.Eq(x => x.Id, blogUpdateDTO.Id);
                var existingBlog = await _blogCollection.Find(filter).FirstOrDefaultAsync();

                if (existingBlog == null)
                    return Response<NoContent>.Fail("Blog not found", 404);

                // Güncelleme DTO'sundan gelen verileri mevcut blog nesnesine aktar
                existingBlog.Title = blogUpdateDTO.Title;
                existingBlog.Content = blogUpdateDTO.Content;
                existingBlog.UpdateDate = blogUpdateDTO.UpdateDate;
                existingBlog.CategoryId = blogUpdateDTO.CategoryId;
                existingBlog.PhotoUrl = blogUpdateDTO.PhotoUrl ?? existingBlog.PhotoUrl;
                // Diğer özellikleri de buraya ekleyin

                // Blogu güncelle
                var updateResult = await _blogCollection.ReplaceOneAsync(filter, existingBlog);

                if (updateResult.IsAcknowledged && updateResult.ModifiedCount > 0)
                {
                    return Response<NoContent>.Success(200);
                }
                else
                {
                    return Response<NoContent>.Fail("Failed to update blog", 500);
                }
            }
            catch (Exception ex)
            {
                return Response<NoContent>.Fail(ex.Message, 500);
            }
        }

    }
}
