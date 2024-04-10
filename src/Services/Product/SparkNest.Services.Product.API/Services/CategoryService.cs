using AutoMapper;
using MongoDB.Driver;
using SparkNest.Common.DTOs;
using SparkNest.Services.ProductAPI.DTOs;
using SparkNest.Services.ProductAPI.Models;
using SparkNest.Services.ProductAPI.Settings;

namespace SparkNest.Services.ProductAPI.Services
{
    public class CategoryService : ICategoryService
    {
        IMongoCollection<Category> _categoryCollection;
        IMapper _mapper;
        IDatabaseSettings _databaseSettings;

        public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            _mapper = mapper;
            _databaseSettings = databaseSettings;
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
        }
        public async Task<Response<List<CategoryDTO>>> GetAllAsync()
        {
            var categoires =await _categoryCollection.Find(x => true).ToListAsync();
            return Response<List<CategoryDTO>>.Success(_mapper.Map<List<CategoryDTO>>(categoires), 200);
        }
        public async Task<Response<CategoryDTO>> CreateAsync(CategoryDTO categoryDTO)
        {
            await _categoryCollection.InsertOneAsync(_mapper.Map<Category>(categoryDTO));
            return Response<CategoryDTO>.Success(categoryDTO, 202);
        }
        public async Task<Response<bool>> DelteAsync(string categoryId)
        {
            await _categoryCollection.FindOneAndDeleteAsync(x=>x.Id==categoryId);
            return Response<bool>.Success(true, 202);
        }
        public async Task<Response<CategoryDTO>> GetByIdAsync(string Id)
        {
            var category = await _categoryCollection.Find<Category>(x => x.Id == Id).FirstOrDefaultAsync();
            if (category == null)
            {
                return Response<CategoryDTO>.Fail("product doesnt exists!", 404);
            }
            return Response<CategoryDTO>.Success(_mapper.Map<CategoryDTO>(category), 200);
        }
    }
}
