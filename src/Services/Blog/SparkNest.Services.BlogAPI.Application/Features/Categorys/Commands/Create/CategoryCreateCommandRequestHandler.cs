using MediatR;
using SparkNest.Common.DTOs;
using SparkNest.Services.BlogAPI.Application.Interfaces;

namespace SparkNest.Services.BlogAPI.Application.Features.Categorys.Commands.Create
{
    public class CategoryCreateCommandRequestHandler : IRequestHandler<CategoryCreateCommandRequest, Response<bool>>
    {
        ICategoryService _categoryService;

        public CategoryCreateCommandRequestHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<Response<bool>> Handle(CategoryCreateCommandRequest request, CancellationToken cancellationToken)
        {

            var result = await _categoryService.CreateAsync(new DTOs.CategoryDTO
            {
                Name = request.Name,
            });
            return result;
        }
    }
}
