using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.UI.MVC.Application.Features.Messages.Commands.SetIsAnswered
{
    public class SetIsAnsweredCommandRequest : IRequest<bool>
    {
        public int Id { get; set; }
        public bool IsAnswered { get; set; }
    }
}
