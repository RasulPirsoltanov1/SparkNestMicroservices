using Amazon.Runtime.Internal.Auth;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SparkNest.Common.ControllerBases;
using SparkNest.Services.BlogAPI.Application.DTOs;
using SparkNest.Services.BlogAPI.Application.Features.Blogs.Commands.Create;
using SparkNest.Services.BlogAPI.Application.Features.Blogs.Commands.Delete;
using SparkNest.Services.BlogAPI.Application.Features.Blogs.Commands.Update;
using SparkNest.Services.BlogAPI.Application.Features.Blogs.Queries.GetAll;
using SparkNest.Services.BlogAPI.Application.Features.Blogs.Queries.GetById;

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
        [HttpGet("{blogId}")]
        public async Task<IActionResult> GetByBlogId(string blogId)
        {
            var result = await _mediator.Send(new BlogsGetByIdQueryRequest()
            {
                BlogId=blogId
            });
            return CreateActionResultInstance(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(BlogCreateRequest blogCreateRequest)
        {
            var result = await _mediator.Send(blogCreateRequest);
            return CreateActionResultInstance(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(BlogsUpdateCommandRequest blogsUpdateCommandRequest)
        {
            var result = await _mediator.Send(blogsUpdateCommandRequest);
            return CreateActionResultInstance(result);
        }




        [HttpDelete("{blogId}")]
        public async Task<IActionResult> Delete(string blogId)
        {
            var result = await _mediator.Send(new BlogDeleteCommandRequest()
            {
                BlogId = blogId
            });
            return CreateActionResultInstance(result);
        }
    }
}
