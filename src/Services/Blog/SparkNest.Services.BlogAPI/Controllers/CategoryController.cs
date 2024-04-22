using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SparkNest.Common.ControllerBases;
using SparkNest.Services.BlogAPI.Application.Features.Categorys.Commands.Create;
using SparkNest.Services.BlogAPI.Application.Features.Categorys.Commands.Delete;
using SparkNest.Services.BlogAPI.Application.Features.Categorys.Queries.GetAll;

namespace SparkNest.Services.BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : CustomControllerBase
    {
        IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateCommandRequest categoryCreateCommandRequest)
        {
            var result = await _mediator.Send(categoryCreateCommandRequest);
            return CreateActionResultInstance(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new  CategoryGetAllQueryRequest()); 
            return CreateActionResultInstance(result);
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> Delete(string categoryId)
        {
            var result = await _mediator.Send(new CategoryDeleteCommandRequest
            {
                CategoryId = categoryId
            });
            return CreateActionResultInstance(result);
        }
    }
}
