using CloudShift.Domain.Model;

namespace CloudShift.Domain
{
    public static class SeedData 
    {
       public static void Seeder(CloudShiftDbContext dbContext)
        {
            SeedGuidEntities(dbContext);
            dbContext.SaveChanges();
        }

        private static void SeedGuidEntities(CloudShiftDbContext dbContext)
        {
            dbContext.GuidEntities.AddRange(new List<GuidEntity>
            {
                new GuidEntity { GuidValue = Guid.NewGuid() },
                new GuidEntity { GuidValue = Guid.NewGuid() }
            });
        }
    }
}
