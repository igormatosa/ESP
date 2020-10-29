using Aucxis.Eprw.Reporting.Dataservice.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aucxis.Eprw.Reporting.Dataservice
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TestEntity> TestEntities { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestEntity>(entity =>
            {
                entity.HasKey(e => new { e.Id });
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                
            });

            

        }

        
    }
}
