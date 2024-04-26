using MediatR;
using SparkNest.Common.DTOs;
using SparkNest.Services.BlogAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.Services.BlogAPI.Application.Features.Subscribers.Queries.GetAll
{
    public class SubscriberGetAllQueryRequest:IRequest<Response<List<Subscriber>>>
    {
    }
}
