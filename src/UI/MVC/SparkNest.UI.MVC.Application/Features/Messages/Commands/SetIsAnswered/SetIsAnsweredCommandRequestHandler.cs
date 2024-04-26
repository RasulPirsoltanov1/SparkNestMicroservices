using MediatR;
using Microsoft.EntityFrameworkCore;
using SparkNest.UI.MVC.Infrastructure.Data;

namespace SparkNest.UI.MVC.Application.Features.Messages.Commands.SetIsAnswered
{
    public class SetIsAnsweredCommandRequestHandler : IRequestHandler<SetIsAnsweredCommandRequest, bool>
    {
        AppDbContext _appDbContext;

        public SetIsAnsweredCommandRequestHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> Handle(SetIsAnsweredCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var message = await _appDbContext.ChatMessages.Where(x=>x.ClientConnectionId==request.ClientId).ToListAsync();
                if (message == null)
                    return false;
                message.ForEach(x =>
                {
                    x.IsAnswered = true;
                });
                _appDbContext.ChatMessages.UpdateRange(message);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
