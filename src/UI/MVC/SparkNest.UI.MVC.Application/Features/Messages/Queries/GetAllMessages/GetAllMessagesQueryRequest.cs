using MediatR;
using Microsoft.EntityFrameworkCore;
using SparkNest.UI.MVC.Domain.Entities;
using SparkNest.UI.MVC.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.UI.MVC.Application.Features.Messages.Queries.GetAllMessages
{
    public class GetAllMessagesQueryRequest:IRequest<List<ChatMessage>>
    {
    }
    public class GetAllMessagesQueryRequestHandler : IRequestHandler<GetAllMessagesQueryRequest, List<ChatMessage>>
    {
        AppDbContext _appDbContext;

        public GetAllMessagesQueryRequestHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<ChatMessage>> Handle(GetAllMessagesQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var twentyFiveSecondsAgo = DateTime.Now.AddSeconds(-120);

                var messages = await _appDbContext.ChatMessages
                    .Where(x => x.IsAnswered == false && x.CreateDate >= twentyFiveSecondsAgo)
                    .ToListAsync();
                return messages;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return new List<ChatMessage>();
            }
        }
    }
}
