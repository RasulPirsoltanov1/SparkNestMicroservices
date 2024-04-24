using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.UI.MVC.Application.Features.Subscribers.Commands
{
    public class SubscriberCreateCommandRequest : IRequest<bool>
    {
        [EmailAddress]
        public string Email { get; set; }
    }
}
