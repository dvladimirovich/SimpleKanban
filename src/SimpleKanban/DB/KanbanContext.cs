using SimpleKanban.DB.Configurations;
using SimpleKanban.DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace SimpleKanban.DB
{
    public class KanbanContext : DbContext
    {
        public KanbanContext(DbContextOptions<KanbanContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Card> Cards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new CardConfiguration());
            modelBuilder.Entity<Card>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
