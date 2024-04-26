using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SparkNest.Common.ControllerBases;
using SparkNest.Services.BlogAPI.Application.Features.Subscribers.Commands.Create;
using SparkNest.Services.BlogAPI.Application.Features.Subscribers.Queries.GetAll;

namespace SparkNest.Services.BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriberController : CustomControllerBase
    {
        IMediator _mediator;

        public SubscriberController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new SubscriberGetAllQueryRequest());
            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SubscriberCreateCommandRequest subscriberCreateCommandRequest)
        {
            var response = await _mediator.Send(subscriberCreateCommandRequest);
            return CreateActionResultInstance(response);
        }
    }
}
