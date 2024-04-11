using MediatR;
using Microsoft.AspNetCore.SignalR;
using SparkNest.UI.MVC.Application.Features.Messages.Commands.Create;
using SparkNest.UI.MVC.Models;

namespace SparkNest.UI.MVC.Hubs
{
    public class ChatHub : Hub
    {
        IMediator _mediator;

        public ChatHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Sending message to Admin or Moderators from Clients
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="clientConnectionId"></param>
        /// <param name="message"></param>
        /// <param name="phone"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task SendToAdmin(string userName, string clientConnectionId, string message, string phone, string email)
        {
            await _mediator.Send(new CreateMessageCommandRequest()
            {
                ClientConnectionId = clientConnectionId,
                Message = message,
                Phone = phone,
                Email = email
            });
            await Clients.All.SendAsync("SendToAdminMessage", userName, clientConnectionId, message);
        }


        /// <summary>
        /// Send message to Client from Admin Page
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="clientConnectionId"></param>
        /// <param name="message"></param>
        /// <returns> <see cref="Task"/></returns>
        public async Task SendToClient(string userName, string clientConnectionId, string message)
        {
            await Clients.Client(clientConnectionId).SendAsync("SendToClientMessage", userName, clientConnectionId, message);
        }


        /// <summary>
        /// Get Current User Connection Id
        /// </summary>
        /// <returns><see cref="string"/></returns>
        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }
    }
}
