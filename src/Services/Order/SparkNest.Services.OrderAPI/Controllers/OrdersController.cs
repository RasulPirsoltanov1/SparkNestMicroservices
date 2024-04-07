using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SparkNest.Common.Base.Services;
using SparkNest.Common.ControllerBases;
using SparkNest.Services.OrderAPI.Application.Features.Orders.Commands.Create;
using SparkNest.Services.OrderAPI.Application.Features.Orders.Commands.StatusChange;
using SparkNest.Services.OrderAPI.Application.Features.Orders.Queries;
using SparkNest.Services.OrderAPI.Application.Features.Orders.Queries.Get;

namespace SparkNest.Services.OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : CustomControllerBase
    {
        IMediator _mediator;
        ISharedIdentityService _sharedIdentityService;
        public OrdersController(IMediator mediator, ISharedIdentityService sharedIdentityService)
        {
            _mediator = mediator;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetByUserId()
        {
            return CreateActionResultInstance(await _mediator.Send(new GetOrderByUserIdQuery { UserId = _sharedIdentityService.UserId }));
        }
        [HttpPost]
        public async Task<IActionResult> Post(CreateOrderCommand createOrderCommand)
        {
            var result = await _mediator.Send(createOrderCommand);
            return CreateActionResultInstance(result);
        }

        [HttpPost("StatusChange")]
        public async Task<IActionResult> StatusChange(StatusChangeOrderCommand statusChangeOrderCommand)
        {
            var result = await _mediator.Send(statusChangeOrderCommand);
            return CreateActionResultInstance(result);
        }
    }
}
