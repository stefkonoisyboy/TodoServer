using Microsoft.EntityFrameworkCore;
using TodoListRestApi.Data.Models;

namespace TodoListRestApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public ApplicationDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder
                .UseSqlServer("Server=.;Database=TodoListWasm;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        public virtual DbSet<Todo> Todos { get; set; }

        public virtual DbSet<ApplicationUser> Users { get; set; }
    }
}
