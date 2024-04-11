using Microsoft.EntityFrameworkCore;
using SparkNest.UI.MVC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.UI.MVC.Infrastructure.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions):base(dbContextOptions)
        {
            
        }
        public DbSet<ChatMessage> ChatMessages { get; set; } 
    }
}
