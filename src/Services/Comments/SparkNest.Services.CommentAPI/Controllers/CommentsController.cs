using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SparkNest.Common.ControllerBases;
using SparkNest.Services.CommentAPI.Application.Features.Comments.Commands.CreateComment;
using SparkNest.Services.CommentAPI.Application.Features.Comments.Queries.GetAllComments;

namespace SparkNest.Services.CommentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : CustomControllerBase
    {
        IMediator _mediator;

        public CommentsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCommentsRequest());
            return CreateActionResultInstance(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentRequest createCommentRequest)
        {
            var result = await _mediator.Send(createCommentRequest);
            return CreateActionResultInstance(result);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await _mediator.Send(new DeleteCommentRequest
            {
                Id=Id
            });
            return CreateActionResultInstance(result);
        }
    }
}
