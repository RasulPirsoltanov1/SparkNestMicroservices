using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using SparkNest.Services.CommentAPI.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.Services.CommentAPI.Infrastructure.Data
{
    public class CommentDbContext : DbContext
    {
        public CommentDbContext(DbContextOptions<CommentDbContext> dbContextOptions) : base(dbContextOptions)
        {
            var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
            if (!databaseCreator.CanConnect())
                databaseCreator.Create();
            if (!databaseCreator.HasTables())
                databaseCreator.HasTables();
        }
        public DbSet<Comment> Comments { get; set; }
    }
}
