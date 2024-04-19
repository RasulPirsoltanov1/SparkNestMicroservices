using Amazon.Runtime.Internal.Auth;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SparkNest.Common.ControllerBases;
using SparkNest.Services.BlogAPI.Application.Features.Blogs.Commands.Create;
using SparkNest.Services.BlogAPI.Application.Features.Blogs.Commands.Delete;
using SparkNest.Services.BlogAPI.Application.Features.Blogs.Queries.GetAll;

namespace SparkNest.Services.BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : CustomControllerBase
    {
        IMediator _mediator;
        public BlogController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new BlogGetAllQueryRequest());
            return CreateActionResultInstance(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BlogCreateRequest blogCreateRequest)
        {
            var result = await _mediator.Send(blogCreateRequest);
            return CreateActionResultInstance(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(BlogDeleteCommandRequest blogDeleteCommandRequest)
        {
            var result = await _mediator.Send(blogDeleteCommandRequest);
            return CreateActionResultInstance(result);
        }
    }
}
