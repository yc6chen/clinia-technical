using Microsoft.EntityFrameworkCore;

namespace TechnicalTest.Project.Infrastructure
{
    public class TestDbContext : DbContext 
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}