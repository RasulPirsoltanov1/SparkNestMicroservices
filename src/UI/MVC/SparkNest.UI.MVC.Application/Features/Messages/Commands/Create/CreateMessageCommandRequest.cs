using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.UI.MVC.Application.Features.Messages.Commands.Create
{
    public class CreateMessageCommandRequest : IRequest<bool>
    {
        public string? UserName { get; set; }

        public string? ClientConnectionId { get; set; }

        public string? Message { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }
    }
}
