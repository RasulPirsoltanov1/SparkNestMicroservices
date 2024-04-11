using MediatR;
using SparkNest.UI.MVC.Domain.Entities;
using SparkNest.UI.MVC.Infrastructure.Data;

namespace SparkNest.UI.MVC.Application.Features.Messages.Commands.Create
{
    public class CreateMessageCommandRequestHanadler : IRequestHandler<CreateMessageCommandRequest, bool>
    {
        AppDbContext _appDbContext;

        public CreateMessageCommandRequestHanadler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> Handle(CreateMessageCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var message = new ChatMessage
                {
                    CreateDate = DateTime.Now,
                    Email = request.Email,
                    ClientConnectionId = request.ClientConnectionId,
                    IsAnswered = false,
                    Message = request.Message,
                    Phone = request.Phone,
                    UserName = request.UserName,
                };

                await _appDbContext.ChatMessages.AddAsync(message);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return false;
            }
        }
    }
}
