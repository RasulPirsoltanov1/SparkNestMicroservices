using SparkNest.UI.MVC.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.UI.MVC.Application.Abstractions
{
    public interface ICommentService
    {
        Task<bool> Create(CreateCommentDTO createCommentDTO);
        Task<List<CreateCommentDTO>> GetAll();
        Task<List<CreateCommentDTO>> GetAllByProductId(string productId);
    }
}
