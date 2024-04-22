using MediatR;
using SparkNest.Common.DTOs;
using SparkNest.Services.BlogAPI.Application.Interfaces;

namespace SparkNest.Services.BlogAPI.Application.Features.Categorys.Commands.Delete
{
    public class CategoryDeleteCommandRequestHandler : IRequestHandler<CategoryDeleteCommandRequest, Response<bool>>
    {
        ICategoryService _categoryService;

        public CategoryDeleteCommandRequestHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<Response<bool>> Handle(CategoryDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _categoryService.DeleteAsync(request.CategoryId);
            return result;
        }
    }
}
