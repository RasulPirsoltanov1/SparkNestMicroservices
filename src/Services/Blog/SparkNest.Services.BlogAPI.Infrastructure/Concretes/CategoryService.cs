using MongoDB.Bson;
using MongoDB.Driver;
using SparkNest.Common.DTOs;
using SparkNest.Services.BlogAPI.Application.DTOs;
using SparkNest.Services.BlogAPI.Application.Interfaces;
using SparkNest.Services.BlogAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.Services.BlogAPI.Infrastructure.Concretes
{
    public class CategoryService : ICategoryService
    {
        IMongoDatabase _database;
        IMongoCollection<Category> _categories;
        IMongoCollection<Blog> _blogs;

        public CategoryService(IMongoDatabase database)
        {
            _database = database;
            _blogs = _database.GetCollection<Blog>(nameof(Blog));
            _categories = _database.GetCollection<Category>(nameof(Category));
        }

        public async Task<Response<bool>> CreateAsync(CategoryDTO category)
        {
            try
            {
                await _categories.InsertOneAsync(new Category
                {
                    Blogs = category.Blogs,
                    CreateDate = DateTime.Now,
                    Name = category.Name,
                });
                return Response<bool>.Success(true, 200);
            }
            catch (Exception ex)
            {
                return Response<bool>.Fail(ex.Message, 500);
            }
        }

        public async Task<Response<bool>> DeleteAsync(string categoryId)
        {
            try
            {
                var deleteFilter = Builders<Category>.Filter.Eq(x => x.Id, categoryId);
                await _categories.DeleteOneAsync(deleteFilter);
                return Response<bool>.Success(true, 200);
            }
            catch (Exception ex)
            {
                return Response<bool>.Fail(ex.Message, 500);
            }
        }

        public Task<Response<bool>> DeletePhotoAsync(string categoryId, string photoUrl)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<CategoryDTO>>> GetAllAsync()
        {
            try
            {
                var categories = await _categories.Find(new BsonDocument()).ToListAsync();
                var categoryDTOs = new List<CategoryDTO>();

                foreach (var category in categories)
                {
                    var categoryDTO = new CategoryDTO
                    {
                        Id = category.Id,
                        CreateDate = category.CreateDate,
                        Name = category.Name,
                        UpdateDate = category.UpdateDate
                    };

                    // Kategorinin her bir blogunu bul ve CategoryDTO'ya ekle
                    var blogs = await _blogs.Find(x => x.CategoryId == category.Id).ToListAsync();
                    categoryDTO.Blogs = blogs;
                    categoryDTO.BlogsCount = blogs.Count;
                    categoryDTOs.Add(categoryDTO);
                }

                return Response<List<CategoryDTO>>.Success(categoryDTOs, 200);
            }
            catch (Exception ex)
            {
                return Response<List<CategoryDTO>>.Fail(ex.Message, 500);
            }
        }


        public async Task<Response<CategoryDTO>> GetByIdAsync(string categoryId)
        {
            try
            {
                var category = await _categories.Find(x => x.Id == categoryId).FirstOrDefaultAsync();

                if (category == null)
                    return Response<CategoryDTO>.Fail("Category not found", 404);

                var categoryDTO = new CategoryDTO
                {
                    Id = category.Id,
                    CreateDate = category.CreateDate,
                    Name = category.Name,
                    UpdateDate = category.UpdateDate
                };
                var blogs = await _blogs.Find(x => x.CategoryId == category.Id).ToListAsync();
                categoryDTO.Blogs = blogs;
                return Response<CategoryDTO>.Success(categoryDTO, 200);
            }
            catch (Exception ex)
            {
                return Response<CategoryDTO>.Fail(ex.Message, 500);
            }
        }

        public async Task<Response<NoContent>> UpdateAsync(CategoryUpdateDTO categoryUpdateDTO)
        {
            try
            {
                // Güncellenecek kategorinin ID'sini ObjectId türüne dönüştür

                // Kategoriyi güncellemek için önce mevcut kategoriyi al
                var filter = Builders<Category>.Filter.Eq(x => x.Id, categoryUpdateDTO.Id);
                var existingCategory = await _categories.Find(filter).FirstOrDefaultAsync();

                if (existingCategory == null)
                    return Response<NoContent>.Fail("Category not found", 404);

                // Güncelleme DTO'sundan gelen verileri mevcut kategori nesnesine aktar
                existingCategory.Name = categoryUpdateDTO.Name;
                existingCategory.UpdateDate = DateTime.Now;
                // Diğer özellikleri de buraya ekleyin

                // Kategoriyi güncelle
                var updateResult = await _categories.ReplaceOneAsync(filter, existingCategory);

                if (updateResult.IsAcknowledged && updateResult.ModifiedCount > 0)
                {
                    return Response<NoContent>.Success(200);
                }
                else
                {
                    return Response<NoContent>.Fail("Failed to update category", 500);
                }
            }
            catch (Exception ex)
            {
                return Response<NoContent>.Fail(ex.Message, 500);
            }
        }

    }
}
