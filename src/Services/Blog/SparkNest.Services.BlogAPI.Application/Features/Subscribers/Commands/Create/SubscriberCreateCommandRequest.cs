using MediatR;
using SparkNest.Common.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.Services.BlogAPI.Application.Features.Subscribers.Commands.Create
{
    public class SubscriberCreateCommandRequest : IRequest<Response<bool>>
    {
        [EmailAddress]
        public string? Email { get; set; }
    }
}
