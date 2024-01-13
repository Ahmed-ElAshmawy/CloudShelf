using CloudShift.Domain.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CloudShift.Domain
{
    public class CloudShiftDbContext : IdentityDbContext<IdentityUser>
    {
        public CloudShiftDbContext(DbContextOptions<CloudShiftDbContext> options) : base(options)
        { 
        }

        public DbSet<GuidEntity> GuidEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GuidEntity>(g =>
            {
                g.HasKey(e => e.Id);
                g.Property(e => e.Id).ValueGeneratedOnAdd();
            });
            
            //modelBuilder.Entity<GuidEntity>().HasData(
            //    new GuidEntity { Id = 1, GuidValue = Guid.NewGuid() },
            //    new GuidEntity { Id = 2 ,GuidValue = Guid.NewGuid() }
            //);
        }
    }
}
