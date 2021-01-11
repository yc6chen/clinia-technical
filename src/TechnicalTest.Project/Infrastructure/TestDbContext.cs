using Microsoft.EntityFrameworkCore;
using TechnicalTest.Project.Domain;

namespace TechnicalTest.Project.Infrastructure
{
    public class TestDbContext : DbContext 
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HealthFacility>();
            
            modelBuilder.Entity<Practitioner>(builder =>
            {
                builder.HasOne(x => x.HealthFacility).WithMany();
            });

            modelBuilder.Entity<Service>(builder =>
            {
                builder.HasMany(x => x.PractitionerServices).WithOne(x => x.Service);
                builder.HasMany(x => x.HealthFacilityServices).WithOne(x => x.Service);
            });

            modelBuilder.Entity<PractitionerService>(builder =>
            {
                builder.HasKey(x => new {x.ServiceId, x.PractitionerId});
                builder.HasOne(x => x.Practitioner).WithMany(x => x.PractitionerServices);
            });
            
            modelBuilder.Entity<HealthFacilityService>(builder =>
            {
                builder.HasKey(x => new {x.ServiceId, x.HealthFacilityId});
                builder.HasOne(x => x.HealthFacility).WithMany(x => x.HealthFacilityServices);
            });
        }
    }
}