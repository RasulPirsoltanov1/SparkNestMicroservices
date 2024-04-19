using MediatR;
using SparkNest.Common.DTOs;
using SparkNest.Services.BlogAPI.Application.DTOs;
using SparkNest.Services.BlogAPI.Application.Interfaces;

namespace SparkNest.Services.BlogAPI.Application.Features.Categorys.Queries.GetAll
{
    public class CategoryGetAllQueryRequestHandler : IRequestHandler<CategoryGetAllQueryRequest, Response<List<CategoryDTO>>>
    {
        ICategoryService _categoryService;

        public CategoryGetAllQueryRequestHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<Response<List<CategoryDTO>>> Handle(CategoryGetAllQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _categoryService.GetAllAsync();
            return result;
        }
    }
}
