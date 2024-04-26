using MediatR;
using Microsoft.AspNetCore.Mvc;
using SparkNest.UI.MVC.Application.Features.Messages.Commands.SetIsAnswered;
using SparkNest.UI.MVC.Application.Features.Messages.Queries.GetAllMessages;

namespace SparkNest.UI.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HelpCenterController : Controller
    {
        IMediator _mediator;

        public HelpCenterController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            return View( await _mediator.Send(new GetAllMessagesQueryRequest()));
        }

        [HttpPost]
        public async Task<IActionResult> Answer(string clientConnectionId, string message, string userName)
        {
            await _mediator.Send(new SetIsAnsweredCommandRequest()
            {
                ClientId=clientConnectionId,
                IsAnswered=true
            });
            ViewBag.UserName = userName;
            ViewBag.ClientConnectionId = clientConnectionId;
            ViewBag.Message = message;
            return View();
        }
    }
}
