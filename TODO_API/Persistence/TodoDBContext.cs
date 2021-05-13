using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODO_API.Controllers.Resources;

namespace TODO_API.Persistence
{
    public class TodoDBContext : DbContext
    {
        public TodoDBContext(DbContextOptions<TodoDBContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem_Details> TodoItem_Details { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TodoItem_Details>().HasKey(ur => new { ur.Item_id});
        }
    }
}
