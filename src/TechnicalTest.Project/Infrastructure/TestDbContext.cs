using FileContextCore;
using FileContextCore.FileManager;
using FileContextCore.Serializer;
using Microsoft.EntityFrameworkCore;
using TechnicalTest.Domain;

namespace TechnicalTest.Infrastructure
{
    public class TestDbContext : DbContext 
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        {
            
        }
        
        public DbSet<HealthFacility> HealthFacilities { get; set; }
        
        public DbSet<Practitioner> Practitioners { get; set; }

        public DbSet<Service> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HealthFacility>(builder =>
            {
                // builder.HasMany(x => x.Practitioners).WithOne(x => x.HealthFacility);
                // builder.HasMany(x => x.Services);
            });
            
            modelBuilder.Entity<Practitioner>(builder =>
            {
                builder.HasMany(x => x.Services);
            });

            modelBuilder.Entity<Service>();
        }
    }
}